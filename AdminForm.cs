using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtrsMapService_User
{
    public partial class AdminForm : Form
    {
        public static string ExPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string JsonPath = ExPath + "\\rtrsjson.json";
        public Data ActualData { get; set; }
        public DateTime starttime;
        public AdminForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = DefaultMaximumSize;
        }
        public async Task<Data> Downloader_tower()
        {
            Data li;
            var req = $"https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/nodes?";
            li = await Task.Run(() =>
            {
                try
                {
                    using (var wb = new WebClient())
                    {
                        var response = wb.DownloadString(req);
                        li = JsonConvert.DeserializeObject<Data>(response);
                    }
                    var stream = JsonPath;
                    {
                        TextWriter tr = new StreamWriter(stream, false);
                        tr.Write(JsonConvert.SerializeObject(li));
                        tr.Flush();
                        tr.Close();
                    }
                    setlog("Локальная база вышек скачена и обновлена.\t Время обновления:" + li.Time + Environment.NewLine);
                    btn_download_multi.Enabled = true;
                    return li;
                }
                catch (Exception ex)
                {
                    setlog(ex.Message + Environment.NewLine);
                    return null;
                }
            });
            return li;

        }
        public void setlog(string txt)
        {
            log_box.Invoke(new Action(() => { log_box.AppendText(txt); log_box.ScrollToCaret(); }));
        }
        private async void dwnld_base_btn_Click(object sender, EventArgs e)
        {
            ActualData = await Downloader_tower();
            dt_time_actual.Text = ActualData.Time.ToString();
        }

        private void btn_download_multi_Click(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            Task.Run(() =>
            {
                foreach (var item in ActualData.li)
                {
                    try
                    {
                        Task.Delay(10);
                        item.GetInfoLoadItem();
                        if (item.id_trans1 != 0)
                            setlog("Tower №" + item.id + "\t" + "Recived data 1 multi" + "\t" + (DateTime.Now - starttime).ToString() + Environment.NewLine);
                        if (item.id_trans2 != 0)
                            setlog("Tower №" + item.id + "\t" + "Recived data 2 multi" + "\t" + (DateTime.Now - starttime).ToString() + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        log_box.Text += "Ошибка " + ex.Message + "\t" + item.id + Environment.NewLine;
                    }

                }
            });
        }

        private void start_generator_btn_Click(object sender, EventArgs e)
        {

        }
    }
}

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

        public AdminForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = DefaultMaximumSize;
        }
        public Data Downloader_tower()
        {
            Data li;
            var req = $"https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/nodes?";
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
                log_box.Text += "Локальная база вышек скачена и обновлена.\t Время обновления:" + li.Time + Environment.NewLine;
                btn_download_multi.Enabled = true;
                return li;
            }
            catch (Exception ex)
            {
                log_box.Text += ex.Message + Environment.NewLine;
                return null;
            }
        }
        private void dwnld_base_btn_Click(object sender, EventArgs e)
        {
            ActualData = Downloader_tower();
        }

        private void btn_download_multi_Click(object sender, EventArgs e)
        {

        }

        private void start_generator_btn_Click(object sender, EventArgs e)
        {

        }
    }
}

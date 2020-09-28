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
        //public static string ExPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string JsonPath = LoadItem.ExPath + "\\rtrsjson.json";
        public Data ActualData { get; set; }
        public DateTime starttime;
        BindingSource bss;
        public AdminForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = DefaultMaximumSize;
            bss = new BindingSource();
            ActualData = new Data();
            bss.DataSource = ActualData.li;
            base_dgrv.AutoGenerateColumns = false;
            base_dgrv.DataSource = bss;
            base_dgrv.Columns[0].DataPropertyName = "id";
            base_dgrv.Columns[1].DataPropertyName = "id_trans1";
            base_dgrv.Columns[2].DataPropertyName = "id_trans2";
            this.Load += AdminForm_Load;
            search_rez = new List<Point>();

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(JsonPath))
            {
                TextReader tr = new StreamReader(JsonPath, false);
                ActualData = JsonConvert.DeserializeObject<Data>(tr.ReadToEnd());
                tower_check.Checked = true;
                bss.DataSource = ActualData.li; bss.ResetBindings(false); base_dgrv.Refresh();
                setlog($"Загружена база. Время обновления: {ActualData.Time.ToString()}\n");
                dt_time_actual.Text = ActualData.Time.ToString();
                btn_download_multi.Enabled = true;
            }
            else
            {
                tower_check.Checked = false;
                setlog($"База данных не найдена на локальном компьютере\n");
                btn_download_multi.Enabled = false;
            }
        }
        private void SaveJson(Data li)
        {
            var stream = JsonPath;
            {
                TextWriter tr = new StreamWriter(stream, false);
                tr.Write(JsonConvert.SerializeObject(li));
                tr.Flush();
                tr.Close();
            }
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

            //await Task.Run(() =>
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         base_dgrv.Invoke(new Action(() => { bss.Add(new LoadItem() { id = i, id_trans1 = i + 1, id_trans2 = i + 2 }); }));
            //         Task.Delay(1000);
            //     }
            // });

            setlog("Получение базы, ожидайте..." + Environment.NewLine);


            ActualData = await Downloader_tower();
            if (ActualData == null)
            {
                setlog("Ошибка закгрузки базы вышек..." + Environment.NewLine);
                return;
            }
            else
            {
                btn_download_multi.Enabled = true;
            }
            bss.DataSource = ActualData.li;
            bss.ResetBindings(false);
            setlog("Локальная база вышек скачена и обновлена. Время обновления: " + ActualData.Time.ToString() + Environment.NewLine);
            dt_time_actual.Text = ActualData.Time.ToString();
            base_dgrv.Refresh();
        }
        private void dgr_placer(LoadItem li)
        {
        }

        private void btn_download_multi_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(LoadItem.ImgMapPath))
            {
                Directory.CreateDirectory(LoadItem.ImgMapPath);
            }
            starttime = DateTime.Now;
            var cnt = ActualData.li.Count();
            var t_s = ActualData.li.Where(q => q.id <= cnt / 2).ToList();
            var t_e = ActualData.li.Where(q => q.id > cnt / 2).ToList();
            //2 потока на загрузку
            Task.Run(() =>
            {
                foreach (var item in t_s)
                {
                    try
                    {
                        Task.Delay(10);
                        item.GetInfoLoadItem();
                        base_dgrv.Invoke(new Action(() => { bss.DataSource = ActualData.li; bss.ResetBindings(false); base_dgrv.Refresh(); }));
                        if (item.id_trans1 != 0)
                            setlog("Tower №" + item.id + "\t" + "Recived data 1 multi" + "\t" + (DateTime.Now - starttime).ToString() + Environment.NewLine);
                        if (item.id_trans2 != 0)
                            setlog("Tower №" + item.id + "\t" + "Recived data 2 multi" + "\t" + (DateTime.Now - starttime).ToString() + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        setlog("Ошибка " + ex.Message + "\t" + item.id + Environment.NewLine);
                    }
                }
            });
            Task.Run(() =>
            {
                foreach (var item in t_e)
                {
                    try
                    {
                        Task.Delay(10);
                        item.GetInfoLoadItem();
                        base_dgrv.Invoke(new Action(() => { bss.DataSource = ActualData.li; bss.ResetBindings(false); base_dgrv.Refresh(); }));
                        if (item.id_trans1 != 0)
                            setlog("Tower №" + item.id + "\t" + "Recived data 1 multi" + "\t" + (DateTime.Now - starttime).ToString() + Environment.NewLine);
                        if (item.id_trans2 != 0)
                            setlog("Tower №" + item.id + "\t" + "Recived data 2 multi" + "\t" + (DateTime.Now - starttime).ToString() + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        setlog("Ошибка " + ex.Message + "\t" + item.id + Environment.NewLine);
                    }
                }
            });
        }

        private void start_generator_btn_Click(object sender, EventArgs e)
        {

        }

        private async void get_act_date_Click(object sender, EventArgs e)
        {
            var t = await Downloader_tower();
            current_time_twr_txt.Text = t.Time.ToString();
            setlog("Время последнего обновления баз на сервере RTRS: " + ActualData.Time.ToString() + Environment.NewLine);
        }
        private LoadItem ActiveLoadItem;
        private void base_dgrv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearPlexInfo();
            var id = (int)base_dgrv[e.ColumnIndex, e.RowIndex].Value;
            if (id == 0) return;
            LoadItem t = null;//new LoadItem();
            if (e.ColumnIndex == 1)
            {
                t = ActualData.li.Where(it => it.id_trans1 == id).Single();

            }
            else if (e.ColumnIndex == 2)
            {
                t = ActualData.li.Where(it => it.id_trans2 == id).Single();
                if (t.map_trans2 != null)
                {
                    string q = LoadItem.ImgMapPath + Path.GetFileName(t.map_trans2.map);
                    if (File.Exists(q))
                        img_trans_item_pb.Image = Image.FromFile(q);
                }
            }
            else if (e.ColumnIndex == 0)
            {
                t = ActualData.li.Where(it => it.id == id).Single();
                tabControl1.SelectedIndex = 1;
            }
            if (t != null)
            {
                ActiveLoadItem = t;
                tower_lat_txt.Text = t.lat.ToString();
                tower_lon_txt.Text = t.lon.ToString();
                fili_rch.Text = t.web_fili;
                place_rch.Text = t.web_place;
                ShowPlexInfo(t, e.ColumnIndex);
                show_1st_info_btn.Enabled = t.id_trans1 != 0 ? true : false;
                show_2st_info_btn.Enabled = t.id_trans2 != 0 ? true : false;
            }

        }

        void ClearPlexInfo()
        {
            LoadItem t = new LoadItem();

            tower_lat_txt.Text = t.lat.ToString();
            tower_lon_txt.Text = t.lon.ToString();
            fili_rch.Text = t.web_fili;
            place_rch.Text = t.web_place;
            show_1st_info_btn.Enabled = t.id_trans1 != 0 ? true : false;
            show_2st_info_btn.Enabled = t.id_trans2 != 0 ? true : false;

            img_trans_item_pb.Image = null;
            plex_ne_lat_txt.Text = string.Empty;
            plex_ne_lon_txt.Text = string.Empty;
            plex_sw_lat_txt.Text = string.Empty;
            plex_sw_lon_txt.Text = string.Empty;
            file_name_txt.Text = string.Empty;
            tvk_txt.Text = string.Empty;
            GC.Collect();
        }
        void ShowPlexInfo(LoadItem t, int plex_num)
        {

            if (t != null)
            {
                mapobj plex = new mapobj();
                if (plex_num == 1)
                    plex = t.map_trans1;
                else if (plex_num == 2)
                    plex = t.map_trans2;
                else
                    return;

                string q = LoadItem.ImgMapPath + Path.GetFileName(plex.map);
                if (File.Exists(q))
                    img_trans_item_pb.Image = Image.FromFile(q);
                plex_ne_lat_txt.Text = plex.nex.ToString();
                plex_ne_lon_txt.Text = plex.ney.ToString();
                plex_sw_lat_txt.Text = plex.swx.ToString();
                plex_sw_lon_txt.Text = plex.swy.ToString();
                file_name_txt.Text = plex.web_name;
                tvk_txt.Text = plex.web_tvk;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveJson(ActualData);
        }

        private void show_1st_info_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            if ((sender as Button).Tag.ToString() == "1")
            {
                ShowPlexInfo(ActiveLoadItem, 1);
            }
            else
            {
                ShowPlexInfo(ActiveLoadItem, 2);
            }
        }
        private List<Point> search_rez;
        private int curr_in_search = -1;

        private void search_txt_TextChanged(object sender, EventArgs e)
        {
            search_rez = new List<Point>();
            //base_dgrv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //base_dgrv.MultiSelect = true; 
            try
            {
                foreach (DataGridViewRow row in base_dgrv.Rows)
                {
                    row.Selected = false;
                    foreach (DataGridViewCell item in row.Cells)
                    {
                        if (item.Value.ToString().Contains(search_txt.Text))
                        {
                            //row.Selected = true;
                            search_rez.Add(new Point(item.ColumnIndex, item.RowIndex));
                            break;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            searh_in_base(0);
        }
        void searh_in_base(int dir)
        {
            if(dir == 0)
                curr_in_search++;
            else
                curr_in_search--;
            if (curr_in_search > search_rez.Count - 1)
            {
                curr_in_search = search_rez.Count -1;
            }
            if (curr_in_search < 0)
            {
                curr_in_search = 0;
            }

            if(search_rez.Count > 0)
            {
                base_dgrv.FirstDisplayedScrollingRowIndex = search_rez[curr_in_search].Y;
                base_dgrv.Rows[search_rez[curr_in_search].Y].Selected = true;
            }
            else
            {
                base_dgrv.FirstDisplayedScrollingRowIndex = 0;
                base_dgrv.Rows[0].Selected = true;
            }

        }
        private void search_f_btn_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Tag.ToString() == "forward")
                searh_in_base(0);
            else
                searh_in_base(1);
        }
    }
}

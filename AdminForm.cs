using HtmlAgilityPack;
using Newtonsoft.Json;
using RtrsMapService_User.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RtrsMapService_User.StaticInfo;

namespace RtrsMapService_User
{
    public partial class AdminForm : Form
    {
        public static string JsonPath = LoadItem.ExPath + "\\rtrsjson.json";
        static string mult1path = LoadItem.ExPath + "\\csv_table\\mult1.csv";
        static string mult2path = LoadItem.ExPath + "\\csv_table\\mult2.csv";
        static string htmlplex1path = LoadItem.ExPath + "\\server\\resources\\1st_mult.html";
        static string htmlplex2path = LoadItem.ExPath + "\\server\\resources\\2end_mult.html";
        public Data ActualData { get; set; }
        public List<ImgItemInfo> ServerImgList { get; set; }
        Timer _Timer { get; set; }
        Timer _RefreshDGR { get; set; }
        private bool fl_isrun { get; set; }
        private bool check_fl_isrun()
        {
            if (fl_isrun)
            {
                MessageBox.Show("Ожидайте выполнение задачи!");
                return true;
            }
            return false;
        }
        public DateTime StartTime { get; set; }
        bool _is_browser;
        public AdminForm(bool is_browser = true)
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = DefaultMaximumSize;
            ActualData = new Data();
            base_dgrv.AutoGenerateColumns = false;
            base_dgrv.DataSource = ActualData.li;
            base_dgrv.Columns[0].DataPropertyName = "id";
            base_dgrv.Columns[1].DataPropertyName = "id_trans1";
            base_dgrv.Columns[2].DataPropertyName = "id_trans2";
            Ev_ResponseServerState += AdminForm_Ev_ResponseServerState; ;
            this.Load += AdminForm_Load;
            search_rez = new List<Point>();
            _Timer = new Timer() { Interval = 100000 };
            _RefreshDGR = new Timer() { Interval = 1200 };
            _RefreshDGR.Tick += _RefreshDGR_Tick;
            _Timer.Tick += _Timer_Tick;
            _is_browser = is_browser;
            //Task.Delay(3000);
            StaticInfo.Ev_CloseMainWindow += StaticInfo_Ev_CloseMainWindow;

        }

        private void StaticInfo_Ev_CloseMainWindow()
        {
            this.Close();
        }

        private void _RefreshDGR_Tick(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadData();
            StaticInfo.DoGetServerState();
            if (!_is_browser)
                status_server_txt.Text = "Не запущен";
        }
        #region BASELOADER
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
        private async void dwnld_base_btn_Click(object sender, EventArgs e)
        {
            if (check_fl_isrun())
                return;
            else
                fl_isrun = true;
            //BlockUI(false);
            setlog("Получение базы, ожидайте..." + Environment.NewLine);
            if (ActualData != new Data())
                if (MessageBox.Show($"Текущая база будет стерта! Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    ActualData = new Data();
                    RefreshDataGridView();
                    //BeginInvoke(new Action(() => { bss.DataSource = ActualData.li; bss.ResetBindings(false); base_dgrv.Refresh(); }));
                }
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
            //bss.DataSource = ActualData.li;
            //bss.ResetBindings(false);
            RefreshDataGridView();
            setlog("Локальная база вышек скачена и обновлена. Время обновления: " + ActualData.Time.ToString() + Environment.NewLine);
            dt_time_actual.Text = ActualData.Time.ToString();
            tower_check.Checked = true;
            base_dgrv.Refresh();
            SaveJson(ActualData);
            fl_isrun = false;
            //BlockUI(true);
        }
        /// <summary>
        /// Узнать дату обновления на сервере
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void get_act_date_Click(object sender, EventArgs e)
        {
            if (check_fl_isrun())
                return;
            else
                fl_isrun = true;
            setlog("Проверка времени последнего обновления базы на сервере RTRS начата..." + Environment.NewLine);
            var t = await Downloader_tower();
            current_time_twr_txt.Text = t.Time.ToString();
            setlog("Время последнего обновления баз на сервере RTRS: " + t.Time.ToString() + Environment.NewLine);
            fl_isrun = false;
        }
        #endregion
        #region PLEXLOADER
        private async Task<List<ImgItemInfo>> ForImgCheckLoader()
        {
            List<ImgItemInfo> ListIII = new List<ImgItemInfo>();
            try
            {
                HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
                using (var wb = new WebClient())
                {
                    var response = await wb.DownloadStringTaskAsync("https://xn--80aa2azak.xn--p1aadc.xn--p1ai//images//map/?C=M;O=D");
                    HD.LoadHtml(response);
                }
                bool a = false, b = false, c = false;
                DateTime dt = new DateTime();
                double size_byte = 0;
                int i = 0;
                string nm = string.Empty;
                foreach (HtmlNode table in HD.DocumentNode.SelectNodes("//table"))
                {
                    foreach (HtmlNode row in table.SelectNodes("tr"))
                    {
                        foreach (HtmlNode cell in row.SelectNodes("th|td"))
                        {
                            var cel = cell.InnerText.Trim();
                            if (cel.Contains(".png") | cel.Contains(".PNG") | cel.Contains(".gif") | cel.Contains(".GIF") |
                                cel.Contains(".jpg") | cel.Contains(".JPG"))
                            {
                                nm = cel;
                                a = true;
                            }
                            if (Regex.IsMatch(cel, @"^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}$"))
                            {
                                dt = DateTime.ParseExact(cel, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                                b = true;
                            }
                            var t = Regex.Match(cel, @"^\d+(.\d)?(?'size'\D?)$");
                            if (t.Success)
                            {
                                var gr = t.Groups["size"].Value;
                                string z = "";
                                if (!string.IsNullOrEmpty(gr))
                                {
                                    z = t.Value.Replace(gr, "");
                                    if (gr.ToLower() == "m")
                                        size_byte = double.Parse(z, CultureInfo.InvariantCulture) * 1024 * 1024;
                                    else if (gr.ToLower() == "k")
                                        size_byte = double.Parse(z, CultureInfo.InvariantCulture) * 1024;
                                }
                                else
                                {
                                    z = t.Value;
                                    size_byte = double.Parse(z, CultureInfo.InvariantCulture);
                                }
                                c = true;
                                //else if(string.IsNullOrEmpty(gr))
                            }
                            if (a && b && c)
                            {
                                ImgItemInfo iii = new ImgItemInfo(nm, dt, size_byte);
                                ListIII.Add(iii);
                                nm = string.Empty;
                                dt = new DateTime();
                                size_byte = 0;
                                a = b = c = false;
                            }
                        }
                        i++;
                    }
                }
                return ListIII;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                GC.Collect(1, GCCollectionMode.Forced);
            }

        }
        private async void btn_download_multi_Click(object sender, EventArgs e)
        {
            if (check_fl_isrun())
                return;
            else
                fl_isrun = true;
            bool use_not_full = false;


            /* ситуации 
             * нет id трансмиттера следовательно и объекта
             * есть id нет изображения (или самого объекта?)
             */
            var notfulldata = ActualData.li.Where(q => q.id_trans1 == 0 || q.id_trans2 == 0).ToList();
            notfulldata.AddRange(ActualData.li.Where(q => q.map_trans1 == null || q.map_trans2 == null).ToList());
            notfulldata.AddRange(ActualData.li.Where(q => (q.map_trans1 != null && string.IsNullOrEmpty(q.map_trans1.map))
            || (q.map_trans2 != null && string.IsNullOrEmpty(q.map_trans2.map))).ToList());
            notfulldata = notfulldata.Distinct().ToList();
            if (MessageBox.Show($"Найдено {notfulldata.Count} неполных записей!\nПопробовать загрузить их?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                use_not_full = true;
            }
            else
            {
                if (MessageBox.Show($"Текущая информация о мультиплексах будет стерта! Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    ActualData.li.ForEach(Q => Q.ClearMapObj());
                    RefreshDataGridView();
                }
            }
            int cnt = ActualData.li.Count();
            List<LoadItem> t_s;
            List<LoadItem> t_e;
            if (use_not_full)
            {
                cnt = notfulldata.Count();
                t_s = notfulldata.Where((c, i) => i % 2 == 0).ToList();//(q => q.id <= cnt / 2).ToList();
                t_e = notfulldata.Where((c, i) => i % 2 != 0).ToList();//(q => q.id > cnt / 2).ToList();
            }
            else
            {
                //t_s = ActualData.li.Where();
                t_s = ActualData.li.Where((c, i) => i % 2 == 0).ToList();
                t_e = ActualData.li.Where((c, i) => i % 2 != 0).ToList();
            }




            //KATALOG INFO
            var qq = Path.GetPathRoot(LoadItem.ExPath);
            var dd = DriveInfo.GetDrives().ToList();
            var a = dd.Where(w => w.Name == qq).First();

            DirectoryInfo di = new DirectoryInfo(LoadItem.ImgMapPath);
            var cursize = di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            cursize = cursize / 1024 / 1024;

            _Timer.Start();
            _RefreshDGR.Start();
            progressBar1.Value = 0;
            progressBar1.Maximum = cnt + 1;
            setlog("Загрузка информации начата. Время начала: " + DateTime.Now.ToString() + Environment.NewLine);

            setlog("Получение базы изображений..." + Environment.NewLine);
            ServerImgList = await ForImgCheckLoader();
            var sum = ServerImgList.Sum(q => q.size_mb);
            if (a.AvailableFreeSpace / (1024 * 1024) - sum < 1000)
            {
                if (MessageBox.Show($"Размер банка фотографий = {(int)sum} MB\nРазмер локальной папки с изображениями = { cursize } MB\n" +
    $"Доступно на диске = {a.AvailableFreeSpace / (1024 * 1024)} MB\nПродолжить загрузку?",
    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! UDALIT'!
            //TextWriter tr = new StreamWriter(LoadItem.ExPath + "\\some.json", false);
            //tr.Write(JsonConvert.SerializeObject(ServerImgList));
            //tr.Flush();
            //tr.Close();

            setProgress();
            setlog($"Загружен список изображений с сервера. Количесвто изображений {ServerImgList.Count}" + Environment.NewLine);
            if (!Directory.Exists(LoadItem.ImgMapPath))
            {
                Directory.CreateDirectory(LoadItem.ImgMapPath);
            }
            StartTime = DateTime.Now;
            await Task.WhenAll(LoadWorkerRunner(t_s), LoadWorkerRunner(t_e));

            ActualData.PlexLoad = DateTime.Now;
            last_multi_download.Text = ActualData.PlexLoad.ToString();
            multi_check.Checked = true;
            setlog($"Загрузка инофрмации о мультиплексах завершена. Время завершения: {ActualData.PlexLoad}" + Environment.NewLine);
            start_generator_btn.Enabled = true;
            SaveJson(ActualData);
            _Timer.Stop();
            _RefreshDGR.Stop();
            //BlockUI(true);
            fl_isrun = false;
        }
        async Task<bool> LoadWorkerRunner(List<LoadItem> li)
        {

            bool t = await Task.Run(() => LoadWorker(li));
            return t;
        }
        bool LoadWorker(List<LoadItem> li)
        {
            foreach (var item in li)
            {
                try
                {
                    Task.Delay(10);
                    item.GetInfoLoadItem(ServerImgList);
                    if (item.id_trans1 != 0)
                        setlog("Tower №" + item.id + "\t" + "Recived data 1 multi" + "\t" + (DateTime.Now - StartTime).ToString() + Environment.NewLine);
                    if (item.id_trans2 != 0)
                        setlog("Tower №" + item.id + "\t" + "Recived data 2 multi" + "\t" + (DateTime.Now - StartTime).ToString() + Environment.NewLine);
                    //BeginInvoke(new Action(() => { bss.DataSource = ActualData.li; bss.ResetBindings(false); base_dgrv.Refresh(); }));
                }
                catch (Exception ex)
                {
                    setlog("Ошибка " + ex.Message + "\t" + item.id + Environment.NewLine);
                }
                setProgress();
            }
            return true;
        }
        #endregion
        #region MAINTAINCE
        private async void _Timer_Tick(object sender, EventArgs e)
        {
            await Task.Run(() => { SaveJson(ActualData); setlog($"Прогресс сохранен. Время сейва: {DateTime.Now}\r\n"); });
        }
        //private void BlockUI(bool flag)
        //{
        //    var txt = GetAll(this, typeof(TextBox)).ToList();
        //    var chck = GetAll(this, typeof(CheckBox)).ToList();
        //    var btn = GetAll(this, typeof(Button)).ToList();
        //    txt.ForEach(t => t.Invoke(new Action(() => t.Enabled = flag)));
        //    chck.ForEach(t => t.Invoke(new Action(() => t.Enabled = flag)));
        //    btn.ForEach(t => t.Invoke(new Action(() => t.Enabled = flag)));
        //}
        private void LoadData()
        {
            if (File.Exists(JsonPath))
            {
                TextReader tr = new StreamReader(JsonPath, false);
                ActualData = JsonConvert.DeserializeObject<Data>(tr.ReadToEnd());
                tower_check.Checked = true;
                if (ActualData.li.Where(q => q.id_trans1 != 0).Count() > 0)
                {
                    multi_check.Checked = true;
                    start_generator_btn.Enabled = true;
                }
                else
                {
                    multi_check.Checked = false;
                    start_generator_btn.Enabled = false;
                }
                RefreshDataGridView();
                //bss.DataSource = ActualData.li; bss.ResetBindings(false); base_dgrv.Refresh();
                setlog($"Загружена база. Время обновления: {ActualData.Time.ToString()}\n");
                dt_time_actual.Text = ActualData.Time.ToString();
                last_multi_download.Text = ActualData.PlexLoad.ToString();
                btn_download_multi.Enabled = true;
            }
            else
            {
                tower_check.Checked = false;
                multi_check.Checked = false;
                setlog($"База данных не найдена на локальном компьютере\n");
                btn_download_multi.Enabled = false;
                start_generator_btn.Enabled = false;
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
        public void setlog(string txt)
        {
            if (log_box.InvokeRequired)
                BeginInvoke(new Action(() => { log_box.AppendText(txt); log_box.ScrollToCaret(); }));
            else
            {
                log_box.AppendText(txt); log_box.ScrollToCaret();
            }

        }
        public void setProgress()
        {
            if (progressBar1.InvokeRequired)
                progressBar1.Invoke(new Action(() => { progressBar1.Value += 1; }));
            else
                progressBar1.Value += 1;
        }
        private void RefreshDataGridView()
        {
            BeginInvoke(new Action(() =>
            {
                base_dgrv.DataSource = typeof(List<>);
                base_dgrv.DataSource = ActualData.li;
                //base_dgrv.AutoResizeColumns();
                //base_dgrv.Refresh();
            }));

        }
        private void show_gen_btn_Click(object sender, EventArgs e)
        {
            var t = sender as Button;
            if (t.Tag.ToString() == "2")
            {
                System.Diagnostics.Process.Start($"http://localhost:{ServPort}/resources/{Path.GetFileName(htmlplex2path)}");
            }
            else
                System.Diagnostics.Process.Start($"http://localhost:{ServPort}/resources/{Path.GetFileName(htmlplex1path)}");
        }
        #endregion
        #region PLEX_INFO_LOC_DGRW
        private LoadItem ActiveLoadItem;
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
            GC.Collect(1, GCCollectionMode.Forced);
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
                if (plex == null)
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
        private void base_dgrv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 | e.ColumnIndex < 0)
                return;
            ClearPlexInfo();
            var id = (int)base_dgrv[e.ColumnIndex, e.RowIndex].Value;
            if (id == 0) return;
            LoadItem t = null;//new LoadItem();
            if (e.ColumnIndex == 1)
            {
                t = ActualData.li.Where(it => it.id_trans1 == id).Single();
                tabControl1.SelectedIndex = 0;
            }
            else if (e.ColumnIndex == 2)
            {
                t = ActualData.li.Where(it => it.id_trans2 == id).Single();
                tabControl1.SelectedIndex = 0;
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
        #endregion
        #region LOC_DGRW_SEARCH
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
            if (base_dgrv.Rows.Count < 1)
            {
                return;
            }
            if (dir == 0)
                curr_in_search++;
            else
                curr_in_search--;
            if (curr_in_search > search_rez.Count - 1)
            {
                curr_in_search = search_rez.Count - 1;
            }
            if (curr_in_search < 0)
            {
                curr_in_search = 0;
            }

            if (search_rez.Count > 0)
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
        #endregion
        #region MAPGEN
        private void start_generator_btn_Click(object sender, EventArgs e)
        {
            if (check_fl_isrun())
                return;
            else
                fl_isrun = true;
            //BlockUI(false);
            setlog("Генерация html запущена. Ожидайте..." + Environment.NewLine);
            if (sec_mult_gen_check.Checked == true & first_mult_gen_check.Checked == true)
            {
                Generator(MultiSelect.both);
            }
            else if (sec_mult_gen_check.Checked == true & first_mult_gen_check.Checked == false)
            {
                Generator(MultiSelect.second);
            }
            else if (sec_mult_gen_check.Checked == false & first_mult_gen_check.Checked == true)
            {
                Generator(MultiSelect.first);
            }
            else
            {
                setlog("Не выбран тип мультплекса для генерации" + Environment.NewLine);
            }
        }
        public async void Generator(StaticInfo.MultiSelect mu)
        {
            progressBar1.Value = 0;
            setlog("Чтение таблиц декодировки" + Environment.NewLine);
            //List<string> fili2 = new List<string>();
            //List<string> fili1 = new List<string>();
            List<string> fili = new List<string>();
            if(mu == MultiSelect.both)
            {
                fili = GetCSV(ActualData, MultiSelect.first);
                fili.AddRange(GetCSV(ActualData, MultiSelect.second));
                fili = fili.Distinct().ToList();
                fili.Sort();
            }
            switch (mu)
            {
                case MultiSelect.both:
                    progressBar1.Maximum = ActualData.li.Count * 2;
                    setlog("Генерация для двух мультов" + Environment.NewLine);
                    await Task.WhenAll(GenTaskRunner(MultiSelect.first,fili), GenTaskRunner(MultiSelect.second, fili));
                    break;
                case MultiSelect.first:
                    progressBar1.Maximum = ActualData.li.Count;
                    await Task.WhenAll(GenTaskRunner(mu, fili));
                    break;
                case MultiSelect.second:
                    progressBar1.Maximum = ActualData.li.Count;
                    await Task.WhenAll(GenTaskRunner(mu, fili));
                    break;
            }
            fl_isrun = false;
            //BlockUI(true);
            GC.Collect(1, GCCollectionMode.Forced);
        }
        async Task<bool> GenTaskRunner(MultiSelect mu, List<string> fili)
        {
            var b = await Task.Run(() => GenTask(mu,fili));
            return b;
        }
        bool GenTask(MultiSelect mu, List<string> fili)
        {

            switch (mu)
            {
                case MultiSelect.first:
                    setlog("Запуск для первого мульта" + Environment.NewLine);
                    break;
                case MultiSelect.second:
                    setlog("Запуск для второго мульта" + Environment.NewLine);
                    break;
            }
            MapBuilder(ActualData, fili, mu);
            return true;
        }
        public void MapBuilder(Data dta, List<string> FILIAL, MultiSelect blue)
        {
            setlog("Чтение шаблона" + Environment.NewLine);
            string fileContents = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RtrsMapService_User.Resources.mult.html"))
            {
                TextReader tr = new StreamReader(stream);
                fileContents = tr.ReadToEnd();
                tr.Close();
            }
            if (blue == MultiSelect.first)
                fileContents = fileContents.Replace("<title>Покрой.рф</title>", "<title>Покрой.рф. 1ый мульт</title>");
            else
                fileContents = fileContents.Replace("<title>Покрой.рф</title>", "<title>Покрой.рф. 2ой мульт</title>");
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(fileContents);
            var nodes = htmlDoc.DocumentNode.SelectSingleNode("//body/script");
            List<string> endings = new List<string>();
            List<int> end_schet = new List<int>();
            int scht = 0;
            string helps = "";
            setlog("Генерация слоев по филиалам" + Environment.NewLine);
            foreach (var item in FILIAL)
            {
                endings.Add($"var b{scht} = L.layerGroup([]);\n\t");
                end_schet.Add(scht);
                helps += $"\"{item}\": b{scht},";
                scht++;
            }
            string ending_fin = $"var baseMaps = {{{helps}}};\n\tvar overlayMaps ={{\"Фон\": c1,\"Карта гугл(спутник)\": c5,\"Карта гугл(гибрид)\": c4, \"Карта гугл(улицы)\": c2,\"Карта гугл(земля)\": c3,\"" +
                "Карта яндекс(спутник)\": s2, \"Карта яндекс(улицы)\": s1, \"Карта яндекс(гибрид)\": s3};\n\tL.control.layers(overlayMaps, baseMaps).addTo(mapQ3); s1.addTo(mapQ3);\n\t";
            setlog("Запуск процесса распределения" + Environment.NewLine);
            foreach (var item in dta.li)
            {
                //setlog(".");
                setProgress();
                string input = string.Empty;
                switch (blue)
                {
                    case MultiSelect.first:
                        if (item.map_trans1 != null && item.map_trans1 != new mapobj() && item.id_trans1 != 0)
                        {
                            input = item.PrintVar() + " = " + item.map_trans1.ToString();
                            if (input.Contains("[[0,0],[0,0]]"))
                                setlog($"Изображение мультиплекса ID № {item.id_trans1} для вышки ID № {item.id} не найдено" + Environment.NewLine);
                        }
                        else
                        {
                            continue;
                        }
                        break;
                    case MultiSelect.second:
                        if (item.map_trans2 != null && item.map_trans2 != new mapobj() && item.id_trans2 != 0)
                        {
                            input = item.PrintVar() + " = " + item.map_trans2.ToString();
                            if (input.Contains("[[0,0],[0,0]]"))
                                setlog($"Изображение мультиплекса ID № {item.id_trans2} для вышки ID № {item.id} не найдено" + Environment.NewLine);
                        }
                        else
                        {
                            continue;
                        }
                        break;
                }
                if (!string.IsNullOrEmpty(item.filial)) //нету id трансмит то зачем добавлять?
                {
                    var o = FILIAL.IndexOf(item.filial);
                    if(o < 0)
                    {
                        setlog($"Филиал {item.filial} в файле CSV_{blue} не найден. Изображение не будет добавлено" + Environment.NewLine);
                    }
                    else 
                    {
                        endings[o] = endings[o].Replace("])", item.PrintVar() + ",])");
                    }

                }

                input = input.Replace("addTo(map)", "addTo(mapQ3)");
                var qq = nodes.OuterHtml;
                if (input != string.Empty)
                {
                    var s = HtmlTextNode.CreateNode(qq.Replace(nodes.InnerText, nodes.InnerText + input + "\n\t"));
                    nodes = nodes.ParentNode.ReplaceChild(s, nodes);
                }
                continue;
            }
            setlog("Завершение..." + Environment.NewLine);
            var qq1 = nodes.OuterHtml;
            var tqwe = string.Join("", endings);
            var ta1 = "\n\t" + tqwe + ending_fin;
            var s1 = HtmlTextNode.CreateNode(qq1.Replace(nodes.InnerText, nodes.InnerText + ta1));
            nodes = nodes.ParentNode.ReplaceChild(s1, nodes);
            StreamWriter srrww;
            switch (blue)
            {
                case MultiSelect.first:
                    srrww = new StreamWriter(htmlplex1path, false);//LoadItem.ExPath + "\\1ый_мульт.html", false);
                    htmlDoc.Save(srrww);
                    srrww.Close();
                    setlog("Сгенерированный файл сохранен" + Environment.NewLine);
                    setlog("Путь к файлу: " + htmlplex1path + Environment.NewLine);
                    System.Diagnostics.Process.Start($"http://localhost:{ServPort}/resources/{Path.GetFileName(htmlplex1path)}");
                    srrww.Dispose();
                    break;
                case MultiSelect.second:
                    srrww = new StreamWriter(htmlplex2path, false);
                    htmlDoc.Save(srrww);
                    srrww.Close();
                    setlog("Сгенерированный файл сохранен" + Environment.NewLine);
                    setlog("Путь к файлу: " + htmlplex2path + Environment.NewLine);
                    System.Diagnostics.Process.Start($"http://localhost:{ServPort}/resources/{Path.GetFileName(htmlplex2path)}");
                    srrww.Dispose();
                    break;
            }
            //BlockUI(true);
        }

        public List<string> GetCSV(Data liout, MultiSelect mu)
        {
            List<string> FILIAL = new List<string>();
            StreamReader nsr;
            if (mu == MultiSelect.first)
                nsr = new StreamReader(mult1path);
            else
                nsr = new StreamReader(mult2path);
            while (true)
            {
                var rdr = nsr.ReadLine();
                if (rdr == null)
                {
                    break;
                }
                var qqww = rdr.Split(',');
                int b = 0;
                if (mu == MultiSelect.first)
                    b = liout.li.IndexOf(liout.li.Where(y => y.id_trans1.ToString() == qqww[0]).FirstOrDefault());
                else if (mu == MultiSelect.second)
                    b = liout.li.IndexOf(liout.li.Where(y => y.id_trans2.ToString() == qqww[0]).FirstOrDefault());

                if (b >= 0)
                    liout.li[b].filial = qqww[1];
                else
                {
                    if (mu == MultiSelect.first)
                        setlog("Не найдена зона покрытия " + qqww[0] + " в csv фалйе 1ого мульта" + Environment.NewLine);
                    else
                        setlog("Не найдена зона покрытия " + qqww[0] + " в csv фалйе 2ого мульта" + Environment.NewLine);
                }
                if (!FILIAL.Contains(qqww[1]))
                    FILIAL.Add(qqww[1]);
            }
            return FILIAL;
        }
        #endregion
        #region SERVERTHING
        int ServPort = 0;
        SimpleHTTPServer myServer;
        private void start_server_man_Click(object sender, EventArgs e)
        {
            if (ServPort == 0)
                ServPort = int.Parse(port_txt.Text);
            myServer = new SimpleHTTPServer(LoadItem.ServerPath, ServPort);
            Task.Delay(1500);
            StaticInfo.DoGetServerState();

        }
        private void AdminForm_Ev_ResponseServerState(SimpleHTTPServer state)
        {
            BeginInvoke(new Action(() =>
                {
                    if (state.State)
                    {
                        status_server_txt.Text = "Запущен";
                        ServPort = state.Port;
                        port_txt.TextChanged -= port_txt_TextChanged;
                        port_txt.Text = ServPort.ToString();
                        port_txt.TextChanged += port_txt_TextChanged;
                        port_txt.ReadOnly = true;
                        start_server_man.Enabled = false;
                    }
                    else
                    {
                        status_server_txt.Text = "Не запущен";
                        start_server_man.Enabled = true;
                    }
                }));

        }

        private void port_txt_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(port_txt.Text, @"^\d+$"))
            {
                port_txt.ForeColor = Color.Black;
                start_server_man.Enabled = true;
                ServPort = int.Parse(port_txt.Text);
            }
            else
            {
                port_txt.ForeColor = Color.Red;
                start_server_man.Enabled = false;
            }
        }
        #endregion
    }
}

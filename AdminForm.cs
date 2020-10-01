using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RtrsMapService_User.StaticInfo;

namespace RtrsMapService_User
{
    public partial class AdminForm : Form
    {
        //public static string ExPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string JsonPath = LoadItem.ExPath + "\\rtrsjson.json";
        static string mult1path = LoadItem.ExPath + "\\csv_table\\mult1.csv";
        static string mult2path = LoadItem.ExPath + "\\csv_table\\mult2.csv";
        static string htmlplex1path = LoadItem.ExPath + "\\server\\resources\\1ый_мульт.html";
        static string htmlplex2path = LoadItem.ExPath + "\\server\\resources\\2ой_мульт.html";
        public Data ActualData { get; set; }
        public List<ImgItemInfo> ServerImgList { get; set; }

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
        /// <summary>
        /// Узнать дату обновления на сервере
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void get_act_date_Click(object sender, EventArgs e)
        {
            var t = await Downloader_tower();
            current_time_twr_txt.Text = t.Time.ToString();
            setlog("Время последнего обновления баз на сервере RTRS: " + ActualData.Time.ToString() + Environment.NewLine);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveJson(ActualData);
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
            SaveJson(ActualData);
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
                bool a = false, b = false;
                DateTime dt = new DateTime();
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
                            if (a && b)
                            {
                                ImgItemInfo iii = new ImgItemInfo(nm, dt);
                                ListIII.Add(iii);
                                nm = string.Empty;
                                dt = new DateTime();
                                a = b = false;
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
                GC.Collect();
            }

        }
        private async void btn_download_multi_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = ActualData.li.Count + 1;
            setlog("Загрузка информации начата. Время начала: " + DateTime.Now.ToString() + Environment.NewLine);
            ServerImgList =  await ForImgCheckLoader();
            setProgress();
            setlog($"Загружен список изображений с сервера. Количесвто изображений {ServerImgList.Count}" + Environment.NewLine);
            if (!Directory.Exists(LoadItem.ImgMapPath))
            {
                Directory.CreateDirectory(LoadItem.ImgMapPath);
            }
            starttime = DateTime.Now;
            var cnt = ActualData.li.Count();
            var t_s = ActualData.li.Where(q => q.id <= cnt / 2).ToList();
            var t_e = ActualData.li.Where(q => q.id > cnt / 2).ToList();
            //2 потока на загрузку
            Task[] t = new Task[2];
            t[0] = (GetLoadItemInfo_T(t_s));
            t[1] = (GetLoadItemInfo_T(t_e));
            foreach (var item in t)
            {
                item.Start();
            }
            Task.WaitAll(t);
            setlog($"Загрузка инофрмации о мультиплексах завершена. Время завершения: {DateTime.Now}"+ Environment.NewLine);
            SaveJson(ActualData);
        }
        private Task<bool> GetLoadItemInfo_T(List<LoadItem> li)
        {
            Task<bool> t = new Task<bool>(() =>
            {
                foreach (var item in li)
                {
                    try
                    {
                        Task.Delay(10);
                        item.GetInfoLoadItem(ServerImgList);
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
                    setProgress();
                }
                return true;
            });
            return t;
        }
        #endregion
        #region MAINTAINCE
        private void LoadData()
        {
            if (File.Exists(JsonPath))
            {
                TextReader tr = new StreamReader(JsonPath, false);
                ActualData = JsonConvert.DeserializeObject<Data>(tr.ReadToEnd());
                tower_check.Checked = true;
                if (ActualData.li.Where(q => q.id_trans1 != 0).Count() > 0)
                    multi_check.Checked = true;
                else
                    multi_check.Checked = false;
                bss.DataSource = ActualData.li; bss.ResetBindings(false); base_dgrv.Refresh();
                setlog($"Загружена база. Время обновления: {ActualData.Time.ToString()}\n");
                dt_time_actual.Text = ActualData.Time.ToString();
                btn_download_multi.Enabled = true;
            }
            else
            {
                tower_check.Checked = false;
                multi_check.Checked = false;
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
        public void setlog(string txt)
        {
            log_box.Invoke(new Action(() => { log_box.AppendText(txt); log_box.ScrollToCaret(); }));
        }
        public void setProgress()
        {
            progressBar1.Invoke(new Action(() => { progressBar1.Value += 1; }));
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
            setlog("Генерация html запущена" + Environment.NewLine);
            if (sec_mult_gen_check.Checked == true & first_mult_gen_check.Checked == true)
            {
                Generator(MultiSelect.both);
                //textBox2.Text = File.GetLastWriteTime(ExPath + "\\2ой_мульт.html").ToString();
                //textBox1.Text = File.GetLastWriteTime(ExPath + "\\1ый_мульт.html").ToString();
            }
            else if (sec_mult_gen_check.Checked == true & first_mult_gen_check.Checked == false)
            {
                Generator(MultiSelect.first);
                //textBox1.Text = File.GetLastWriteTime(ExPath + "\\1ый_мульт.html").ToString();
                //button1.Enabled = true;
            }
            else if (sec_mult_gen_check.Checked == false & first_mult_gen_check.Checked == true)
            {
                Generator(MultiSelect.second);
                //textBox2.Text = File.GetLastWriteTime(ExPath + "\\2ой_мульт.html").ToString();
                //button2.Enabled = true;
            }
            else
            {
                setlog("Не выбран тип мультплекса для генерации" + Environment.NewLine);
            }
        }
        public void Generator(StaticInfo.MultiSelect mu)
        {
            progressBar1.Value = 0;
            setlog("Чтение таблиц декодировки" + Environment.NewLine);
            List<string> fili2 = new List<string>();
            List<string> fili1 = new List<string>();
            switch (mu)
            {
                case MultiSelect.both:
                    progressBar1.Maximum = ActualData.li.Count * 2;
                    setlog("Генерация для двух мультов" + Environment.NewLine);
                    Task ts = new Task(() =>
                    {
                        fili1 = GetCSV(ActualData, MultiSelect.first);
                        setlog("Запуск для первого мульта" + Environment.NewLine);
                        MapBuilder(ActualData, fili1, StaticInfo.MultiSelect.first);
                    });
                    Task ts1 = new Task(() =>
                    {
                        fili2 = GetCSV(ActualData, MultiSelect.second);
                        setlog("Запуск для второго мульта" + Environment.NewLine);
                        MapBuilder(ActualData, fili2, StaticInfo.MultiSelect.second);
                    });
                    ts.Start();
                    ts1.Start();
                    break;
                case MultiSelect.first:
                    fili1 = GetCSV(ActualData, MultiSelect.first);
                    progressBar1.Maximum = ActualData.li.Count;
                    Task ts2 = new Task(() =>
                    {
                        setlog("Запуск для первого мульта" + Environment.NewLine);
                        MapBuilder(ActualData, fili1, StaticInfo.MultiSelect.first);
                    });
                    ts2.Start();
                    break;
                case MultiSelect.second:
                    fili2 = GetCSV(ActualData, MultiSelect.second);
                    progressBar1.Maximum = ActualData.li.Count;
                    Task ts3 = new Task(() =>
                    {
                        setlog("Запуск для второго мульта" + Environment.NewLine);
                        MapBuilder(ActualData, fili2, StaticInfo.MultiSelect.second);
                    });
                    ts3.Start();
                    break;
            }
            GC.Collect();
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
            FILIAL.Sort();
            setlog("Генерация слоев по филиалам" + Environment.NewLine);
            foreach (var item in FILIAL)
            {
                endings.Add($"var b{scht} = L.layerGroup([]);\n\t");
                end_schet.Add(scht);
                helps += $"\"{item}\": b{scht},";
                scht++;
            }
            string ending_fin = $"var baseMaps = {{{helps}}};\n\tvar overlayMaps ={{\"Фон\": c1,\"Карта гугл(спутник)\": c5,\"Карта гугл(гибрид)\": c4, \"Карта гугл(улицы)\": c2,\"Карта гугл(земля)\": c3,\""+
                "Карта яндекс(спутник)\": y1, \"Карта яндекс(улицы)\": y2, \"Карта яндекс(гибрид)\": y3, \"Карта яндекс(вектор)\": y4 }};\n\tL.control.layers(overlayMaps, baseMaps).addTo(mapQ3);\n\t";
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
                    endings[o] = endings[o].Replace("])", item.PrintVar() + ",])");
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
            setlog("Завершение" + Environment.NewLine);
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
                    srrww.Dispose();
                    break;
                case MultiSelect.second:
                    srrww = new StreamWriter(htmlplex2path, false);
                    htmlDoc.Save(srrww);
                    srrww.Close();
                    setlog("Сгенерированный файл сохранен" + Environment.NewLine);
                    setlog("Путь к файлу: " + htmlplex2path + Environment.NewLine);
                    srrww.Dispose();
                    break;
            }
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

                //if (b >= 0)
                //liout.li[b].filial = qqww[1];
                else
                {
                    if (mu == MultiSelect.first)
                        setlog("Не найдена зона покрытия " + qqww[0] + " в 1ом мульте" + Environment.NewLine);
                    else
                        setlog("Не найдена зона покрытия " + qqww[0] + " во 2ом мульте" + Environment.NewLine);
                }
                if (!FILIAL.Contains(qqww[1]))
                    FILIAL.Add(qqww[1]);
            }
            return FILIAL;
        }
        #endregion
    }
}

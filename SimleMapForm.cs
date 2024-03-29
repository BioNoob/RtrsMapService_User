﻿using CefSharp;
using CefSharp.WinForms;
using RtrsMapService_User.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RtrsMapService_User.StaticInfo;

namespace RtrsMapService_User
{
    public partial class SimleMapForm : Form
    {
        public static int ServPort = 1515;
        public ChromiumWebBrowser chromeBrowser;
        SimpleHTTPServer myServer;
        string selectedimg_path = string.Empty;
        string img_help_file = string.Empty;
        static string ExPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        static string ServerPath = ExPath + "\\server";
        static string mult1path = ServerPath + "\\resources\\MapBuild.html";
        string fileContents = string.Empty;
        bool fl_brsizeset = false;
        Dictionary<TextBox, bool> Dic = new Dictionary<TextBox, bool>();
        Size Zone_to_print = new Size();
        Size _ActSize;
        Size ActSize { set { _ActSize = value; if (fl_isInit) set_h_w_act(); else set_h_w_act_before(); } get { return _ActSize; } }
        System.Windows.Forms.Timer timers;
        public string mult_id = "";

        private void set_h_w_act()
        {
            size_hieght.Invoke(new Action(() => { size_hieght.Text = ActSize.Height.ToString(); }));
            size_width.Invoke(new Action(() => { size_width.Text = ActSize.Width.ToString(); }));
        }
        private void set_h_w_act_before()
        {
            size_hieght.Text = ActSize.Height.ToString();
            size_width.Text = ActSize.Width.ToString();
        }
        Size _NewSize;
        public Size NewSize { get { return _NewSize; } set { _NewSize = value; set_size_btn.Enabled = true; } }

        string standart_string = "html, body {height: 100%;width: 100%;";
        bool fl_isloaded = false;
        private bool fl_isInit = false;

        public SimleMapForm()
        {
            InitializeComponent();
            string q = Path.GetDirectoryName(mult1path);
            if (!Directory.Exists(q))
            {
                Directory.CreateDirectory(q);
            }
            if (!File.Exists(mult1path))
            {
                var t = File.Create(mult1path);
                t.Close();
            }
            this.Shown += MapBorderGeneratorForm_Shown;
            Dic.Add(sw_lat_txt, false);
            Dic.Add(sw_lon_txt, false);
            Dic.Add(ne_lat_txt, false);
            Dic.Add(ne_lon_txt, false);
            show_map_btn.Enabled = false;
            //size_rect_chk.Enabled = false;
            icon_color_cmb.SelectedIndex = 0;
            square_color_cmb.SelectedIndex = 0;
            LoadSett();
            save_img_btn.Enabled = false;
            simplechecker();
            InitializeChromium();
            timers = new System.Windows.Forms.Timer();
            timers.Tick += Timers_Tick;
            timers.Interval = 500;
            set_size_hieght_txt.Text = "0";
            set_size_wifth_txt.Text = "0";
            set_size_btn.Enabled = false;
            StaticInfo.ThrowServerError += StaticInfo_ThrowServerError;
            StaticInfo.Ev_CloseMainWindow += StaticInfo_Ev_CloseMainWindow;
            StaticInfo.Ev_TransferDataToMap += StaticInfo_Ev_TransferDataToMap;
            Thread thr = new Thread(new ThreadStart(InitializeLocalHost));
            thr.Start();
        }

        private void StaticInfo_Ev_TransferDataToMap(mapobj mo, Image img, int id)
        {
            SetInputFromOutside(mo, img, id);
        }

        private void StaticInfo_Ev_CloseMainWindow()
        {
            MapBorderImg_FormClosing(this, new FormClosingEventArgs(CloseReason.FormOwnerClosing, false));
        }

        private void StaticInfo_ThrowServerError()
        {
            this.Invoke(new Action(() => { StaticInfo_Ev_CloseMainWindow(); }));//Close(); }));
            //this.Invoke(new Action(() => { Dispose(); }));
        }

        public void InitializeChromium()
        {
            chromeBrowser = new ChromiumWebBrowser("");
            chromeBrowser.DownloadHandler = new DownloadHandler();
            chromeBrowser.FrameLoadEnd += ChromeBrowser_FrameLoadEnd;
            chromeBrowser.MouseEnter += panel_top_MouseEnter;
            panel3.Controls.Add(chromeBrowser);
        }
        public void InitializeLocalHost()
        {
            //Thread.Sleep(1500);
            myServer = new SimpleHTTPServer(ServerPath, ServPort);
        }

        private async void Timers_Tick(object sender, EventArgs e)
        {
            var t = await chromeBrowser.GetTextAsync();
            coordplacer(t);
        }

        string LastLayerName = string.Empty;


        private void coordplacer(string text)
        {
            Task st = new Task(() =>
            {
                var t_list = text.Split(Environment.NewLine.ToCharArray()).ToList();
                var img_border = t_list.Where(t => t.Contains("imgborder")).SingleOrDefault();
                var zone_border = t_list.Where(t => t.Contains("zoneborder")).SingleOrDefault();
                var img_center = t_list.Where(t => t.Contains("imgcenter")).SingleOrDefault();
                var zone_center = t_list.Where(t => t.Contains("zonecenter")).SingleOrDefault();
                var zone_pixel = t_list.Where(t => t.Contains("zonepixel")).SingleOrDefault();
                var currlayer = t_list.Where(t => t.Contains("currlayer")).SingleOrDefault();
                if (!string.IsNullOrEmpty(currlayer))
                    LastLayerName = currlayer.Replace("currlayer=", "");
                if (string.IsNullOrEmpty(img_border))
                    return;
                img_border = img_border.Replace("imgborder=", "");
                zone_border = zone_border.Replace("zoneborder=", "");
                img_center = img_center.Replace("imgcenter=", "");
                zone_center = zone_center.Replace("zonecenter=", "");
                if (!string.IsNullOrEmpty(zone_pixel))
                {
                    if (!fl_brsizeset)
                    {
                        zone_pixel = zone_pixel.Replace("zonepixel=", "");
                        var zone_border_pix_ = zone_pixel.Split(';');
                        Zone_to_print = new Size(int.Parse(zone_border_pix_[0]), int.Parse(zone_border_pix_[1]));
                        ActSize = Zone_to_print;
                        show_map_btn.Invoke(new Action(() => { show_map_btn.PerformClick(); }));
                        fl_brsizeset = true;
                    }

                }

                var zone_border_ = zone_border.Split('!');
                var zone_border_1 = zone_border_[0].Split(';');
                var zone_border_2 = zone_border_[1].Split(';');
                zone_sw_lat.Invoke(new Action(() => { zone_sw_lat.Text = zone_border_1[0]; }));
                zone_sw_lon.Invoke(new Action(() => { zone_sw_lon.Text = zone_border_1[1]; }));
                zone_ne_lat.Invoke(new Action(() => { zone_ne_lat.Text = zone_border_2[0]; }));
                zone_ne_lon.Invoke(new Action(() => { zone_ne_lon.Text = zone_border_2[1]; }));

                var img_border_ = img_border.Split('!');
                var img_border_1 = img_border_[0].Split(';');
                var img_border_2 = img_border_[1].Split(';');
                img_sw_lat.Invoke(new Action(() => { img_sw_lat.Text = img_border_1[0]; }));
                img_sw_lon.Invoke(new Action(() => { img_sw_lon.Text = img_border_1[1]; }));
                img_ne_lat.Invoke(new Action(() => { img_ne_lat.Text = img_border_2[0]; }));
                img_ne_lon.Invoke(new Action(() => { img_ne_lon.Text = img_border_2[1]; }));



                var img_center_ = img_center.Split(';');
                img_centr_lat.Invoke(new Action(() => { img_centr_lat.Text = img_center_[0]; }));
                img_centr_lon.Invoke(new Action(() => { img_centr_lon.Text = img_center_[1]; }));

                var zone_center_ = zone_center.Split(';');
                zone_centr_lat.Invoke(new Action(() => { zone_centr_lat.Text = zone_center_[0]; }));
                zone_centr_lon.Invoke(new Action(() => { zone_centr_lon.Text = zone_center_[1]; }));


            });
            st.Start();
        }
        void simplechecker()
        {
            var qwe = Dic.Values.Where(qt => qt == false).Count();
            if (qwe > 0)
            {
                show_map_btn.Enabled = false;
            }
            else
            {
                show_map_btn.Enabled = true;
            }
        }
        private async void save_img_btn_Click(object sender, EventArgs e)
        {
            BlockUI(false);
            pictureBox1.Visible = true;
            while (!fl_isloaded)
            {

            }
            await Task.Delay(3000);
            string my_name_is;
            DateTime dt = DateTime.Now;
            if (!string.IsNullOrEmpty(mult_id))
                my_name_is = $"ID_{mult_id}_{dt.ToString("dd_MM-HH:mm")}_MapImg";
            else
                my_name_is = $"{dt.ToString("dd_MM-HH:mm")}_MapImg";
            chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync($"manualPrint(\"{my_name_is}\");");
            SaveSett();
            pictureBox1.Visible = false;
            BlockUI(true);
        }

        private void show_map_btn_Click(object sender, EventArgs e)
        {
            BlockUI(false);
            save_img_btn.Enabled = false;
            //TextReader tr = new StreamReader(mult1path);
            //string fileContents = string.Empty;// = tr.ReadToEnd();
            //if (checkBox1.Checked)
            if (!string.IsNullOrEmpty(selectedimg_path))
                if (!File.Exists(selectedimg_path))
                {
                    MessageBox.Show("Файл изображения не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            //tr.Close();
            WI_HI_cssSetter(ActSize);
            TextWriter tw = new StreamWriter(mult1path, false);
            switch (LastLayerName)
            {
                case "Фон":
                    fileContents = fileContents.Replace("s1.addTo(map); map.options.crs = L.CRS.EPSG3395;", "c1.addTo(map); map.options.crs = L.CRS.EPSG3857;");
                    break;
                case "Карта гугл(спутник)":
                    fileContents = fileContents.Replace("s1.addTo(map); map.options.crs = L.CRS.EPSG3395;", "c5.addTo(map); map.options.crs = L.CRS.EPSG3857;");
                    break;
                case "Карта гугл(гибрид)":
                    fileContents = fileContents.Replace("s1.addTo(map); map.options.crs = L.CRS.EPSG3395;", "c4.addTo(map); map.options.crs = L.CRS.EPSG3857;");
                    break;
                case "Карта гугл(улицы)":
                    fileContents = fileContents.Replace("s1.addTo(map); map.options.crs = L.CRS.EPSG3395;", "c2.addTo(map); map.options.crs = L.CRS.EPSG3857;");
                    break;
                case "Карта гугл(земля)":
                    fileContents = fileContents.Replace("s1.addTo(map); map.options.crs = L.CRS.EPSG3395;", "c3.addTo(map); map.options.crs = L.CRS.EPSG3857;");
                    break;
                case "Карта яндекс(спутник)":
                    fileContents = fileContents.Replace("s1.addTo(map); map.options.crs = L.CRS.EPSG3395;", "s2.addTo(map); map.options.crs = L.CRS.EPSG3395;");
                    break;
                case "Карта яндекс(гибрид)":
                    fileContents = fileContents.Replace("s1.addTo(map); map.options.crs = L.CRS.EPSG3395;", "s3.addTo(map); map.options.crs = L.CRS.EPSG3395;");
                    break;
            }
            fileContents = fileContents.Replace("SWLAT", sw_lat_txt.Text);
            fileContents = fileContents.Replace("SWLON", sw_lon_txt.Text);
            fileContents = fileContents.Replace("NELAT", ne_lat_txt.Text);
            fileContents = fileContents.Replace("NELON", ne_lon_txt.Text);
            fileContents = fileContents.Replace("ZOOM", numericUpDown1.Value.ToString());
            if (icon_check.Checked)
            {
                if (icon_color_cmb.SelectedIndex == 0)
                    fileContents = fileContents.Replace("_ICON_", "myIconRED");
                else
                    fileContents = fileContents.Replace("_ICON_", "myIconBLUE");
            }
            else
            {
                fileContents = fileContents.Replace("_ICON_", "myIconRED");
            }
            if (square_check.Checked)
            {
                if (square_color_cmb.SelectedIndex == 1)
                    fileContents = fileContents.Replace("_COLOR_", "#ff120a80");
                else
                    fileContents = fileContents.Replace("_COLOR_", "#150cff80");
            }
            else
            {
                fileContents = fileContents.Replace("_COLOR_", "#ff120a80");
            }
            if (!string.IsNullOrEmpty(selectedimg_path))
                fileContents = fileContents.Replace("_IMG_PATH_", "true");
            else
                fileContents = fileContents.Replace("_IMG_PATH_", "false");
            fileContents = fileContents.Replace("_DRAG_ZONE_", true.ToString().ToLower());
            fileContents = fileContents.Replace("_DRAG_IMG_", true.ToString().ToLower());
            fileContents = fileContents.Replace("_MARKER_", icon_check.Checked.ToString().ToLower());
            fileContents = fileContents.Replace("_SQUARE_", square_check.Checked.ToString().ToLower());
            selectedimg_path = selectedimg_path.Replace("\\", "/");
            string _path_ = $"http://localhost:{ServPort}\\resources\\" + img_help_file;
            _path_ = _path_.Replace("\\", "/");
            string web_path;
            if (string.IsNullOrEmpty(img_help_file))
                web_path = _path_ + "MapBuild.html";
            else
                web_path = _path_.Replace(img_help_file, "MapBuild.html");
            fileContents = fileContents.Replace("_PATH_", _path_); //selectedimg_path);
            if (force_set_border)
                set_size_by_rect();
            tw.Write(fileContents);
            tw.Close();
            chromeBrowser.Load(web_path);//mult1path);
            fl_isloaded = false;
            save_img_btn.Enabled = false;
        }

        private void set_size_btn_Click(object sender, EventArgs e)
        {
            ActSize = NewSize;
            if (show_map_btn.Enabled)
            {
                show_map_btn.PerformClick();
            }
            SaveSett();
        }

        private void set_size_border_btn_Click(object sender, EventArgs e)
        {
            //set_size_btn.Enabled = !size_rect_chk.Checked;
            //clear_size_btn.Enabled = !size_rect_chk.Checked;
            //if (size_rect_chk.Checked)
            //fl_brsizeset = false;
            //if (show_map_btn.Enabled)
            //{
            //    show_map_btn.PerformClick();
            //}

            NewSize = new Size();
            WI_HI_cssSetter(NewSize);
            force_set_border = true;
            if (show_map_btn.Enabled)
            {
                show_map_btn.PerformClick();
            }
            force_set_border = false;
            set_size_hieght_txt.Text = NewSize.Height.ToString();
            set_size_wifth_txt.Text = NewSize.Width.ToString();
            SaveSett();
        }

        bool force_set_border = false;
        private void clear_size_btn_Click(object sender, EventArgs e)
        {
            //fl_brsizeset = false;
            NewSize = new Size();
            WI_HI_cssSetter(NewSize);
            if (show_map_btn.Enabled)
            {
                show_map_btn.PerformClick();
            }
            //NewSize = new Size();
            //WI_HI_cssSetter(NewSize);
            //force_set_border = true;
            //if (show_map_btn.Enabled)
            //{
            //    show_map_btn.PerformClick();
            //}
            //force_set_border = false;
            //set_size_hieght_txt.Text = NewSize.Height.ToString();
            //set_size_wifth_txt.Text = NewSize.Width.ToString();
            //SaveSett();
        }

        private void select_img_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выбрать изображение";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedimg_path = openFileDialog1.FileName;
                Bitmap bm = new Bitmap(selectedimg_path);
                var ext = Path.GetExtension(selectedimg_path);
                img_help_file = $"buffer{ext}";
                bm.Save(ExPath + $"\\server\\resources\\{img_help_file}");
                file_name_txt.Text = openFileDialog1.SafeFileName;
                //if (checkBox1.Checked)
                //{
                //    img_moving_chk.Enabled = true;
                //}
            }
            simplechecker();
        }

        private void square_check_CheckedChanged(object sender, EventArgs e)
        {
            square_color_cmb.Enabled = !square_color_cmb.Enabled;
            //size_rect_chk.Enabled = square_color_cmb.Enabled;
            //zone_checker();
        }

        private void icon_check_CheckedChanged(object sender, EventArgs e)
        {
            icon_color_cmb.Enabled = !icon_color_cmb.Enabled;
            //zone_checker();
        }

        private void ne_lat_txt_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            string regex = @"^(\+|\-)?\d{1,2}(\.\d+)?$";
            if (Regex.IsMatch(t.Text, regex))
            {
                t.ForeColor = Color.Black;
                Dic[t] = true;
            }
            else
            {
                t.ForeColor = Color.Red;
                Dic[t] = false;
            }
            simplechecker();
        }

        private void sw_lon_txt_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            string regex = @"^(\+|\-)?\d{1,3}(\.\d+)?$";
            if (Regex.IsMatch(t.Text, regex))
            {
                t.ForeColor = Color.Black;
                Dic[t] = true;
            }
            else
            {
                t.ForeColor = Color.Red;
                Dic[t] = false;
            }
            var qwe = Dic.Values.Where(qt => qt == false).Count();
            if (qwe > 0)
                show_map_btn.Enabled = false;
            else
                show_map_btn.Enabled = true;
        }

        private void set_size_hieght_txt_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            string regex = @"^\d+$";
            if (Regex.IsMatch(t.Text, regex))
            {
                t.ForeColor = Color.Black;
                Size bb = new Size();
                var b = int.Parse(t.Text);
                if (t.Tag.ToString() == "hi")
                {
                    bb.Height = b;
                    bb.Width = NewSize.Width;
                    NewSize = bb;
                }
                else
                {
                    bb.Width = b;
                    bb.Height = NewSize.Height;
                    NewSize = bb;
                }
            }
            else
            {
                t.ForeColor = Color.Red;
            }
        }

        private void LoadSett()
        {
            sw_lat_txt.Text = Properties.Settings.Default.SLat.ToString(CultureInfo.InvariantCulture);
            sw_lon_txt.Text = Properties.Settings.Default.SLon.ToString(CultureInfo.InvariantCulture);
            ne_lat_txt.Text = Properties.Settings.Default.NLat.ToString(CultureInfo.InvariantCulture);
            ne_lon_txt.Text = Properties.Settings.Default.NLon.ToString(CultureInfo.InvariantCulture);
            numericUpDown1.Value = Properties.Settings.Default.Zoom;
            icon_check.Checked = Properties.Settings.Default.ic_active;
            square_check.Checked = Properties.Settings.Default.sq_active;
            //size_rect_chk.Checked = Properties.Settings.Default.sqr_cut_fl;
            icon_color_cmb.SelectedIndex = Properties.Settings.Default.ic_ind_color;
            square_color_cmb.SelectedIndex = Properties.Settings.Default.sq_ind_color;
            //zone_moving_chk.Checked = Properties.Settings.Default.zone_move;
            selectedimg_path = Properties.Settings.Default.path_img;
            //img_moving_chk.Checked = Properties.Settings.Default.img_move;
            //checkBox1.Checked = Properties.Settings.Default.img_work_chk;
            openFileDialog1.FileName = selectedimg_path;
            file_name_txt.Text = Path.GetFileName(selectedimg_path);//openFileDialog1.SafeFileName;
            WI_HI_cssSetter(new Size(Properties.Settings.Default.size_w, Properties.Settings.Default.size_h));
            mult_id = Properties.Settings.Default.current_id;
            if (Properties.Settings.Default.mapform_start_pos != new Point())
                this.Location = Properties.Settings.Default.mapform_start_pos;
            else
                this.Location = new Point(this.Location.X + this.Width, this.Location.Y);
            if (Properties.Settings.Default.mapform_size != new Size())
                this.Size = Properties.Settings.Default.mapform_size;
            if (File.Exists(selectedimg_path))
            {

                Bitmap bm = new Bitmap(selectedimg_path);
                var ext = Path.GetExtension(selectedimg_path);
                img_help_file = $"buffer{ext}";
                var t = ExPath + $"\\server\\resources\\{img_help_file}";
                selectedimg_path = selectedimg_path.Replace('/', '\\');
                if (!selectedimg_path.Contains(t))
                    bm.Save(t);
                bm.Dispose();
            }
        }
        private void SaveSett()
        {
            Properties.Settings.Default.SLat = double.Parse(sw_lat_txt.Text, CultureInfo.InvariantCulture);
            Properties.Settings.Default.SLon = double.Parse(sw_lon_txt.Text, CultureInfo.InvariantCulture);
            Properties.Settings.Default.NLat = double.Parse(ne_lat_txt.Text, CultureInfo.InvariantCulture);
            Properties.Settings.Default.NLon = double.Parse(ne_lon_txt.Text, CultureInfo.InvariantCulture);

            Properties.Settings.Default.mapform_size = this.Size;
            Properties.Settings.Default.mapform_start_pos = this.Location;

            Properties.Settings.Default.Zoom = (int)numericUpDown1.Value;
            Properties.Settings.Default.ic_active = icon_check.Checked;
            Properties.Settings.Default.sq_active = square_check.Checked;
            Properties.Settings.Default.ic_ind_color = icon_color_cmb.SelectedIndex;
            Properties.Settings.Default.sq_ind_color = square_color_cmb.SelectedIndex;
            //Properties.Settings.Default.img_work_chk = checkBox1.Checked;
            //Properties.Settings.Default.zone_move = true;
            //Properties.Settings.Default.img_move = true;
            //Properties.Settings.Default.sqr_cut_fl = size_rect_chk.Checked;
            Properties.Settings.Default.path_img = selectedimg_path;
            Properties.Settings.Default.size_w = ActSize.Width;
            Properties.Settings.Default.size_h = ActSize.Height;
            Properties.Settings.Default.Save();
        }

        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            fl_isloaded = true;
            save_img_btn.Invoke(new Action(() => { save_img_btn.Enabled = true; }));
            timers.Stop();
            timers.Start();
            BlockUI(true);
        }
        private void panel_top_MouseEnter(object sender, EventArgs e)
        {
            //this.Focus();
        }

        public void MapBorderImg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var dlg = MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {
                    CloseApp();
                    StaticInfo.DoCloseChildWindow();
                }
                else
                    e.Cancel = true;
                //e.Cancel = true;
                //Hide();
            }
            else
            {
                CloseApp();
            }
        }
        void CloseApp()
        {
            timers.Stop();
            Task.Delay(100);
            timers.Dispose();
            SaveSett();
            chromeBrowser.Dispose();
            myServer.Stop();
            //Application.Exit();
        }
        private void WI_HI_cssSetter(Size si)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RtrsMapService_User.MapBuild_ext.html"))
            {
                TextReader tr = new StreamReader(stream);
                fileContents = tr.ReadToEnd();
                tr.Close();
            }
            if (si == new Size())
            {
                ActSize = panel3.Size;
            }
            else
            {
                string buf = "html, body {height: " + si.Height.ToString() + "px;width: " + si.Width.ToString() + "px;";
                fileContents = fileContents.Replace(standart_string, buf);
                fileContents = fileContents.Replace("<div id=\"map\" style=\"width: X; height: Y\"></div>", $"<div id=\"map\" style=\"width: {si.Width}px; height: {si.Height}px\"></div>");
                ActSize = si;
            }
        }
        private void MapBorderGeneratorForm_Shown(object sender, EventArgs e)
        {
            fl_isInit = true;
        }

        public void SetInputFromOutside(mapobj mo, Image img = null, int id = 0)
        {
            if (img != null)
            {
                CopyImg(img);
            }
            sw_lat_txt.Text = mo.swx.ToString(CultureInfo.InvariantCulture);
            sw_lon_txt.Text = mo.swy.ToString(CultureInfo.InvariantCulture);
            ne_lat_txt.Text = mo.nex.ToString(CultureInfo.InvariantCulture);
            ne_lon_txt.Text = mo.ney.ToString(CultureInfo.InvariantCulture);
            if (id != 0)
                mult_id = id.ToString();
            else
                mult_id = string.Empty;
        }
        private void CopyImg(Image img)
        {
            Bitmap bm = new Bitmap(img);
            //var ext = Program.GetImageFormat(bm);
            img_help_file = $"buffer.png";
            selectedimg_path = ExPath + $"\\server\\resources\\{img_help_file}";
            bm.Save(ExPath + $"\\server\\resources\\{img_help_file}", ImageFormat.Png);
            bm.Dispose();
            file_name_txt.Text = "\'Импортированное\'";
            //if (checkBox1.Checked)
            //{
            //  img_moving_chk.Enabled = true;
            //}
            simplechecker();
        }
        private void set_size_by_rect()
        {
            //if (square_check.Checked)
            {
                string buf = "</script>" + Environment.NewLine +
                             "</body>" + Environment.NewLine +
                             "</html>";
                string buf1 = "var hel = pg_sq._pxBounds.getSize();" + Environment.NewLine +
                    "document.getElementById(\"map\").style.width = hel.x + \"px\";" + Environment.NewLine +
                    "document.getElementById(\"map\").style.height = hel.y + \"px\";" + Environment.NewLine +
                    "map._onResize();" + Environment.NewLine +
                    "document.body.style.width = hel.x + \"px\";" + Environment.NewLine +
                    "document.body.style.height = hel.y + \"px\";" + Environment.NewLine +
                    "document.getElementById(\"zonepixel\").textContent = \"zonepixel=\" + hel.x + \";\" + hel.y;" + Environment.NewLine +
                    "</script>" + Environment.NewLine +
                    "</body>" + Environment.NewLine +
                    "</html>";

                fileContents = fileContents.Replace(buf, buf1);
            }
        }
        private void set_img_btn_Click(object sender, EventArgs e)
        {
            GetBlock(sender as Button);
        }

        private void set_zone_btn_Click(object sender, EventArgs e)
        {
            GetBlock(sender as Button);
        }
        private void GetBlock(Button btn)
        {
            switch (btn.Name)
            {
                case "set_zone_btn":
                    sw_lat_txt.Text = zone_sw_lat.Text;
                    sw_lon_txt.Text = zone_sw_lon.Text;
                    ne_lat_txt.Text = zone_ne_lat.Text;
                    ne_lon_txt.Text = zone_ne_lon.Text;
                    break;
                case "set_img_btn":
                    sw_lat_txt.Text = img_sw_lat.Text;
                    sw_lon_txt.Text = img_sw_lon.Text;
                    ne_lat_txt.Text = img_ne_lat.Text;
                    ne_lon_txt.Text = img_ne_lon.Text;
                    break;
            }
        }
        private void BlockUI(bool flag)
        {
            var txt = GetAll(this, typeof(TextBox)).ToList();
            var chck = GetAll(this, typeof(CheckBox)).ToList();
            var btn = GetAll(this, typeof(Button)).ToList();
            txt.ForEach(t => t.Invoke(new Action(() => t.Enabled = flag)));
            chck.ForEach(t => t.Invoke(new Action(() => t.Enabled = flag)));
            btn.ForEach(t => t.Invoke(new Action(() => t.Enabled = flag)));
        }

        private void clear_img_btn_Click(object sender, EventArgs e)
        {
            selectedimg_path = string.Empty;
            openFileDialog1.FileName = string.Empty;
            file_name_txt.Text = string.Empty;
            SaveSett();
        }
    }
}

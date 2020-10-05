namespace RtrsMapService_User
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.multi_check = new System.Windows.Forms.CheckBox();
            this.last_multi_download = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_download_multi = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.log_box = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sec_mult_gen_check = new System.Windows.Forms.CheckBox();
            this.first_mult_gen_check = new System.Windows.Forms.CheckBox();
            this.start_generator_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.get_act_date = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.current_time_twr_txt = new System.Windows.Forms.TextBox();
            this.tower_check = new System.Windows.Forms.CheckBox();
            this.dt_time_actual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dwnld_base_btn = new System.Windows.Forms.Button();
            this.base_dgrv = new System.Windows.Forms.DataGridView();
            this.id_tower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_trans1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_trans2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.search_b_btn = new System.Windows.Forms.Button();
            this.search_f_btn = new System.Windows.Forms.Button();
            this.search_txt = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tvk_txt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.plex_ne_lon_txt = new System.Windows.Forms.TextBox();
            this.plex_ne_lat_txt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.plex_sw_lon_txt = new System.Windows.Forms.TextBox();
            this.plex_sw_lat_txt = new System.Windows.Forms.TextBox();
            this.file_name_txt = new System.Windows.Forms.TextBox();
            this.img_trans_item_pb = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.show_2st_info_btn = new System.Windows.Forms.Button();
            this.show_1st_info_btn = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.place_rch = new System.Windows.Forms.RichTextBox();
            this.fili_rch = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tower_lon_txt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tower_lat_txt = new System.Windows.Forms.TextBox();
            this.start_server_man = new System.Windows.Forms.Button();
            this.status_server_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.port_txt = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.base_dgrv)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_trans_item_pb)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(92, 336);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(337, 20);
            this.progressBar1.TabIndex = 19;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.multi_check);
            this.groupBox3.Controls.Add(this.last_multi_download);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btn_download_multi);
            this.groupBox3.Location = new System.Drawing.Point(12, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(417, 77);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "База мультиплексов";
            // 
            // multi_check
            // 
            this.multi_check.AutoSize = true;
            this.multi_check.Checked = true;
            this.multi_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.multi_check.Enabled = false;
            this.multi_check.Location = new System.Drawing.Point(8, 19);
            this.multi_check.Name = "multi_check";
            this.multi_check.Size = new System.Drawing.Size(155, 17);
            this.multi_check.TabIndex = 0;
            this.multi_check.TabStop = false;
            this.multi_check.Text = "Наличие локальной базы";
            this.multi_check.UseVisualStyleBackColor = true;
            // 
            // last_multi_download
            // 
            this.last_multi_download.Enabled = false;
            this.last_multi_download.Location = new System.Drawing.Point(212, 44);
            this.last_multi_download.Name = "last_multi_download";
            this.last_multi_download.Size = new System.Drawing.Size(199, 20);
            this.last_multi_download.TabIndex = 4;
            this.last_multi_download.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Дата и время последнего обновления";
            // 
            // btn_download_multi
            // 
            this.btn_download_multi.Location = new System.Drawing.Point(8, 42);
            this.btn_download_multi.Name = "btn_download_multi";
            this.btn_download_multi.Size = new System.Drawing.Size(75, 23);
            this.btn_download_multi.TabIndex = 1;
            this.btn_download_multi.Text = "Загрузить";
            this.btn_download_multi.UseVisualStyleBackColor = true;
            this.btn_download_multi.Click += new System.EventHandler(this.btn_download_multi_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Выходной лог";
            // 
            // log_box
            // 
            this.log_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log_box.Location = new System.Drawing.Point(12, 362);
            this.log_box.Name = "log_box";
            this.log_box.ReadOnly = true;
            this.log_box.Size = new System.Drawing.Size(417, 262);
            this.log_box.TabIndex = 17;
            this.log_box.TabStop = false;
            this.log_box.Text = "";
            this.log_box.ZoomFactor = 0.8F;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.port_txt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.status_server_txt);
            this.groupBox2.Controls.Add(this.start_server_man);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.sec_mult_gen_check);
            this.groupBox2.Controls.Add(this.first_mult_gen_check);
            this.groupBox2.Controls.Add(this.start_generator_btn);
            this.groupBox2.Location = new System.Drawing.Point(12, 206);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 124);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Генерация карт";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(400, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = ".csv файлы раскодировки должны находится в папке \"csv_table\" программы";
            // 
            // sec_mult_gen_check
            // 
            this.sec_mult_gen_check.AutoSize = true;
            this.sec_mult_gen_check.Location = new System.Drawing.Point(8, 48);
            this.sec_mult_gen_check.Name = "sec_mult_gen_check";
            this.sec_mult_gen_check.Size = new System.Drawing.Size(113, 17);
            this.sec_mult_gen_check.TabIndex = 5;
            this.sec_mult_gen_check.Text = "2ой мультиплекс";
            this.sec_mult_gen_check.UseVisualStyleBackColor = true;
            // 
            // first_mult_gen_check
            // 
            this.first_mult_gen_check.AutoSize = true;
            this.first_mult_gen_check.Location = new System.Drawing.Point(8, 19);
            this.first_mult_gen_check.Name = "first_mult_gen_check";
            this.first_mult_gen_check.Size = new System.Drawing.Size(115, 17);
            this.first_mult_gen_check.TabIndex = 4;
            this.first_mult_gen_check.Text = "1ый мультиплекс";
            this.first_mult_gen_check.UseVisualStyleBackColor = true;
            // 
            // start_generator_btn
            // 
            this.start_generator_btn.Location = new System.Drawing.Point(6, 71);
            this.start_generator_btn.Name = "start_generator_btn";
            this.start_generator_btn.Size = new System.Drawing.Size(114, 34);
            this.start_generator_btn.TabIndex = 6;
            this.start_generator_btn.Text = "Сгенерировать";
            this.start_generator_btn.UseVisualStyleBackColor = true;
            this.start_generator_btn.Click += new System.EventHandler(this.start_generator_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.get_act_date);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.current_time_twr_txt);
            this.groupBox1.Controls.Add(this.tower_check);
            this.groupBox1.Controls.Add(this.dt_time_actual);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dwnld_base_btn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 105);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "База вышек";
            // 
            // get_act_date
            // 
            this.get_act_date.BackgroundImage = global::RtrsMapService_User.Properties.Resources.refresh;
            this.get_act_date.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.get_act_date.Location = new System.Drawing.Point(381, 69);
            this.get_act_date.Name = "get_act_date";
            this.get_act_date.Size = new System.Drawing.Size(30, 30);
            this.get_act_date.TabIndex = 8;
            this.get_act_date.UseVisualStyleBackColor = true;
            this.get_act_date.Click += new System.EventHandler(this.get_act_date_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(210, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Дата обновления локальной копии";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // current_time_twr_txt
            // 
            this.current_time_twr_txt.Enabled = false;
            this.current_time_twr_txt.Location = new System.Drawing.Point(169, 75);
            this.current_time_twr_txt.Name = "current_time_twr_txt";
            this.current_time_twr_txt.ReadOnly = true;
            this.current_time_twr_txt.Size = new System.Drawing.Size(206, 20);
            this.current_time_twr_txt.TabIndex = 6;
            this.current_time_twr_txt.TabStop = false;
            // 
            // tower_check
            // 
            this.tower_check.AutoSize = true;
            this.tower_check.Checked = true;
            this.tower_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tower_check.Enabled = false;
            this.tower_check.Location = new System.Drawing.Point(8, 19);
            this.tower_check.Name = "tower_check";
            this.tower_check.Size = new System.Drawing.Size(155, 17);
            this.tower_check.TabIndex = 0;
            this.tower_check.TabStop = false;
            this.tower_check.Text = "Наличие локальной базы";
            this.tower_check.UseVisualStyleBackColor = true;
            // 
            // dt_time_actual
            // 
            this.dt_time_actual.Enabled = false;
            this.dt_time_actual.Location = new System.Drawing.Point(169, 45);
            this.dt_time_actual.Name = "dt_time_actual";
            this.dt_time_actual.Size = new System.Drawing.Size(243, 20);
            this.dt_time_actual.TabIndex = 4;
            this.dt_time_actual.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "Дата и время последнего обновления на сервере";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dwnld_base_btn
            // 
            this.dwnld_base_btn.Location = new System.Drawing.Point(8, 42);
            this.dwnld_base_btn.Name = "dwnld_base_btn";
            this.dwnld_base_btn.Size = new System.Drawing.Size(75, 23);
            this.dwnld_base_btn.TabIndex = 0;
            this.dwnld_base_btn.Text = "Загрузить";
            this.dwnld_base_btn.UseVisualStyleBackColor = true;
            this.dwnld_base_btn.Click += new System.EventHandler(this.dwnld_base_btn_Click);
            // 
            // base_dgrv
            // 
            this.base_dgrv.AllowUserToAddRows = false;
            this.base_dgrv.AllowUserToDeleteRows = false;
            this.base_dgrv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.base_dgrv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.base_dgrv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.base_dgrv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.base_dgrv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_tower,
            this.Id_trans1,
            this.id_trans2});
            this.base_dgrv.Location = new System.Drawing.Point(6, 42);
            this.base_dgrv.MultiSelect = false;
            this.base_dgrv.Name = "base_dgrv";
            this.base_dgrv.ReadOnly = true;
            this.base_dgrv.Size = new System.Drawing.Size(344, 302);
            this.base_dgrv.TabIndex = 6;
            this.base_dgrv.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.base_dgrv_CellMouseDoubleClick);
            // 
            // id_tower
            // 
            this.id_tower.HeaderText = "Id Вышки";
            this.id_tower.Name = "id_tower";
            this.id_tower.ReadOnly = true;
            this.id_tower.Width = 79;
            // 
            // Id_trans1
            // 
            this.Id_trans1.HeaderText = "Id 1ый мульт";
            this.Id_trans1.Name = "Id_trans1";
            this.Id_trans1.ReadOnly = true;
            this.Id_trans1.Width = 97;
            // 
            // id_trans2
            // 
            this.id_trans2.HeaderText = "Id 2ой мульт";
            this.id_trans2.Name = "id_trans2";
            this.id_trans2.ReadOnly = true;
            this.id_trans2.Width = 95;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.search_b_btn);
            this.groupBox4.Controls.Add(this.search_f_btn);
            this.groupBox4.Controls.Add(this.search_txt);
            this.groupBox4.Controls.Add(this.tabControl1);
            this.groupBox4.Controls.Add(this.base_dgrv);
            this.groupBox4.Location = new System.Drawing.Point(435, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(356, 612);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Содержимое";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "Поиск";
            // 
            // search_b_btn
            // 
            this.search_b_btn.BackgroundImage = global::RtrsMapService_User.Properties.Resources.back;
            this.search_b_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.search_b_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.search_b_btn.Location = new System.Drawing.Point(281, 12);
            this.search_b_btn.Name = "search_b_btn";
            this.search_b_btn.Size = new System.Drawing.Size(30, 25);
            this.search_b_btn.TabIndex = 12;
            this.search_b_btn.Tag = "backward";
            this.search_b_btn.UseVisualStyleBackColor = true;
            this.search_b_btn.Click += new System.EventHandler(this.search_f_btn_Click);
            // 
            // search_f_btn
            // 
            this.search_f_btn.BackgroundImage = global::RtrsMapService_User.Properties.Resources.forward;
            this.search_f_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.search_f_btn.Location = new System.Drawing.Point(317, 12);
            this.search_f_btn.Name = "search_f_btn";
            this.search_f_btn.Size = new System.Drawing.Size(30, 25);
            this.search_f_btn.TabIndex = 11;
            this.search_f_btn.Tag = "forward";
            this.search_f_btn.UseVisualStyleBackColor = true;
            this.search_f_btn.Click += new System.EventHandler(this.search_f_btn_Click);
            // 
            // search_txt
            // 
            this.search_txt.Location = new System.Drawing.Point(62, 16);
            this.search_txt.Name = "search_txt";
            this.search_txt.Size = new System.Drawing.Size(121, 20);
            this.search_txt.TabIndex = 9;
            this.search_txt.TabStop = false;
            this.search_txt.TextChanged += new System.EventHandler(this.search_txt_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(6, 350);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 256);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.tvk_txt);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.plex_ne_lon_txt);
            this.tabPage1.Controls.Add(this.plex_ne_lat_txt);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.plex_sw_lon_txt);
            this.tabPage1.Controls.Add(this.plex_sw_lat_txt);
            this.tabPage1.Controls.Add(this.file_name_txt);
            this.tabPage1.Controls.Add(this.img_trans_item_pb);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(317, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Мульт инфо";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(212, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Имя файла";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(212, 202);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "ТВК";
            // 
            // tvk_txt
            // 
            this.tvk_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tvk_txt.Location = new System.Drawing.Point(212, 218);
            this.tvk_txt.Name = "tvk_txt";
            this.tvk_txt.Size = new System.Drawing.Size(100, 20);
            this.tvk_txt.TabIndex = 17;
            this.tvk_txt.TabStop = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "NE долгота";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(212, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "NE широта";
            // 
            // plex_ne_lon_txt
            // 
            this.plex_ne_lon_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plex_ne_lon_txt.Location = new System.Drawing.Point(212, 179);
            this.plex_ne_lon_txt.Name = "plex_ne_lon_txt";
            this.plex_ne_lon_txt.Size = new System.Drawing.Size(100, 20);
            this.plex_ne_lon_txt.TabIndex = 14;
            this.plex_ne_lon_txt.TabStop = false;
            // 
            // plex_ne_lat_txt
            // 
            this.plex_ne_lat_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plex_ne_lat_txt.Location = new System.Drawing.Point(212, 140);
            this.plex_ne_lat_txt.Name = "plex_ne_lat_txt";
            this.plex_ne_lat_txt.Size = new System.Drawing.Size(100, 20);
            this.plex_ne_lat_txt.TabIndex = 13;
            this.plex_ne_lat_txt.TabStop = false;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "SW долгота";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "SW широта";
            // 
            // plex_sw_lon_txt
            // 
            this.plex_sw_lon_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plex_sw_lon_txt.Location = new System.Drawing.Point(212, 100);
            this.plex_sw_lon_txt.Name = "plex_sw_lon_txt";
            this.plex_sw_lon_txt.Size = new System.Drawing.Size(100, 20);
            this.plex_sw_lon_txt.TabIndex = 10;
            this.plex_sw_lon_txt.TabStop = false;
            // 
            // plex_sw_lat_txt
            // 
            this.plex_sw_lat_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plex_sw_lat_txt.Location = new System.Drawing.Point(212, 61);
            this.plex_sw_lat_txt.Name = "plex_sw_lat_txt";
            this.plex_sw_lat_txt.Size = new System.Drawing.Size(100, 20);
            this.plex_sw_lat_txt.TabIndex = 9;
            this.plex_sw_lat_txt.TabStop = false;
            // 
            // file_name_txt
            // 
            this.file_name_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.file_name_txt.Location = new System.Drawing.Point(6, 6);
            this.file_name_txt.Name = "file_name_txt";
            this.file_name_txt.Size = new System.Drawing.Size(200, 20);
            this.file_name_txt.TabIndex = 8;
            this.file_name_txt.TabStop = false;
            // 
            // img_trans_item_pb
            // 
            this.img_trans_item_pb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.img_trans_item_pb.Location = new System.Drawing.Point(6, 32);
            this.img_trans_item_pb.Name = "img_trans_item_pb";
            this.img_trans_item_pb.Size = new System.Drawing.Size(200, 210);
            this.img_trans_item_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.img_trans_item_pb.TabIndex = 7;
            this.img_trans_item_pb.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.show_2st_info_btn);
            this.tabPage2.Controls.Add(this.show_1st_info_btn);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.place_rch);
            this.tabPage2.Controls.Add(this.fili_rch);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.tower_lon_txt);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.tower_lat_txt);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(317, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Вышка инфо";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // show_2st_info_btn
            // 
            this.show_2st_info_btn.Location = new System.Drawing.Point(219, 72);
            this.show_2st_info_btn.Name = "show_2st_info_btn";
            this.show_2st_info_btn.Size = new System.Drawing.Size(92, 41);
            this.show_2st_info_btn.TabIndex = 28;
            this.show_2st_info_btn.TabStop = false;
            this.show_2st_info_btn.Tag = "2";
            this.show_2st_info_btn.Text = "Посмотреть 2ой плекс";
            this.show_2st_info_btn.UseVisualStyleBackColor = true;
            this.show_2st_info_btn.Click += new System.EventHandler(this.show_1st_info_btn_Click);
            // 
            // show_1st_info_btn
            // 
            this.show_1st_info_btn.Location = new System.Drawing.Point(219, 25);
            this.show_1st_info_btn.Name = "show_1st_info_btn";
            this.show_1st_info_btn.Size = new System.Drawing.Size(92, 41);
            this.show_1st_info_btn.TabIndex = 27;
            this.show_1st_info_btn.TabStop = false;
            this.show_1st_info_btn.Tag = "1";
            this.show_1st_info_btn.Text = "Посмотреть 1ый плекс";
            this.show_1st_info_btn.UseVisualStyleBackColor = true;
            this.show_1st_info_btn.Click += new System.EventHandler(this.show_1st_info_btn_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Адрес";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "Филиал";
            // 
            // place_rch
            // 
            this.place_rch.Location = new System.Drawing.Point(10, 159);
            this.place_rch.Name = "place_rch";
            this.place_rch.ReadOnly = true;
            this.place_rch.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.place_rch.Size = new System.Drawing.Size(188, 71);
            this.place_rch.TabIndex = 26;
            this.place_rch.TabStop = false;
            this.place_rch.Text = "";
            // 
            // fili_rch
            // 
            this.fili_rch.Location = new System.Drawing.Point(10, 69);
            this.fili_rch.Name = "fili_rch";
            this.fili_rch.ReadOnly = true;
            this.fili_rch.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.fili_rch.Size = new System.Drawing.Size(188, 71);
            this.fili_rch.TabIndex = 25;
            this.fili_rch.TabStop = false;
            this.fili_rch.Text = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(106, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Долгота";
            // 
            // tower_lon_txt
            // 
            this.tower_lon_txt.Location = new System.Drawing.Point(109, 25);
            this.tower_lon_txt.Name = "tower_lon_txt";
            this.tower_lon_txt.Size = new System.Drawing.Size(89, 20);
            this.tower_lon_txt.TabIndex = 19;
            this.tower_lon_txt.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Широта";
            // 
            // tower_lat_txt
            // 
            this.tower_lat_txt.Location = new System.Drawing.Point(10, 25);
            this.tower_lat_txt.Name = "tower_lat_txt";
            this.tower_lat_txt.Size = new System.Drawing.Size(89, 20);
            this.tower_lat_txt.TabIndex = 17;
            this.tower_lat_txt.TabStop = false;
            // 
            // start_server_man
            // 
            this.start_server_man.Location = new System.Drawing.Point(264, 82);
            this.start_server_man.Name = "start_server_man";
            this.start_server_man.Size = new System.Drawing.Size(146, 23);
            this.start_server_man.TabIndex = 7;
            this.start_server_man.Text = "Форсированный запуск";
            this.start_server_man.UseVisualStyleBackColor = true;
            this.start_server_man.Click += new System.EventHandler(this.start_server_man_Click);
            // 
            // status_server_txt
            // 
            this.status_server_txt.Location = new System.Drawing.Point(266, 32);
            this.status_server_txt.Name = "status_server_txt";
            this.status_server_txt.ReadOnly = true;
            this.status_server_txt.Size = new System.Drawing.Size(145, 20);
            this.status_server_txt.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(262, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Статус локального сервера";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // port_txt
            // 
            this.port_txt.Location = new System.Drawing.Point(337, 58);
            this.port_txt.Name = "port_txt";
            this.port_txt.Size = new System.Drawing.Size(73, 20);
            this.port_txt.TabIndex = 10;
            this.port_txt.Text = "1515";
            this.port_txt.TextChanged += new System.EventHandler(this.port_txt_TextChanged);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(303, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 17);
            this.label18.TabIndex = 11;
            this.label18.Text = "Порт";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 634);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.log_box);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(742, 673);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.base_dgrv)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_trans_item_pb)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox multi_check;
        private System.Windows.Forms.TextBox last_multi_download;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_download_multi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox log_box;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox sec_mult_gen_check;
        private System.Windows.Forms.CheckBox first_mult_gen_check;
        private System.Windows.Forms.Button start_generator_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox tower_check;
        private System.Windows.Forms.TextBox dt_time_actual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button dwnld_base_btn;
        private System.Windows.Forms.Button get_act_date;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox current_time_twr_txt;
        private System.Windows.Forms.DataGridView base_dgrv;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox img_trans_item_pb;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox plex_sw_lon_txt;
        private System.Windows.Forms.TextBox plex_sw_lat_txt;
        private System.Windows.Forms.TextBox file_name_txt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tvk_txt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox plex_ne_lon_txt;
        private System.Windows.Forms.TextBox plex_ne_lat_txt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RichTextBox place_rch;
        private System.Windows.Forms.RichTextBox fili_rch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tower_lon_txt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tower_lat_txt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button show_2st_info_btn;
        private System.Windows.Forms.Button show_1st_info_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tower;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_trans1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_trans2;
        private System.Windows.Forms.TextBox search_txt;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button search_b_btn;
        private System.Windows.Forms.Button search_f_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox status_server_txt;
        private System.Windows.Forms.Button start_server_man;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox port_txt;
    }
}
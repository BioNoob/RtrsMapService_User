namespace RtrsMapService_User
{
    partial class ImageGetter
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageGetter));
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_strip = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.server_name_txt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.plex_id_txt = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.zone_sw_lat = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.get_info_btn = new System.Windows.Forms.Button();
            this.zone_ne_lon = new System.Windows.Forms.TextBox();
            this.zone_sw_lon = new System.Windows.Forms.TextBox();
            this.save_img_btn = new System.Windows.Forms.Button();
            this.zone_ne_lat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.out_img = new System.Windows.Forms.PictureBox();
            this.lon_center_txt = new System.Windows.Forms.TextBox();
            this.lat_center_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.transfer_btn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.out_img)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.MarqueeAnimationSpeed = 30;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_strip,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 545);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(343, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_strip
            // 
            this.status_strip.ForeColor = System.Drawing.SystemColors.Highlight;
            this.status_strip.Name = "status_strip";
            this.status_strip.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.status_strip.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.status_strip.Size = new System.Drawing.Size(176, 17);
            this.status_strip.Spring = true;
            this.status_strip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 10;
            this.toolTip1.AutoPopDelay = 1000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.ReshowDelay = 5;
            // 
            // server_name_txt
            // 
            this.server_name_txt.Location = new System.Drawing.Point(12, 26);
            this.server_name_txt.Name = "server_name_txt";
            this.server_name_txt.ReadOnly = true;
            this.server_name_txt.Size = new System.Drawing.Size(188, 20);
            this.server_name_txt.TabIndex = 7;
            this.toolTip1.SetToolTip(this.server_name_txt, "имя на сервере");
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.plex_id_txt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 28);
            this.panel1.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "ID мульта";
            // 
            // plex_id_txt
            // 
            this.plex_id_txt.Location = new System.Drawing.Point(73, 4);
            this.plex_id_txt.Name = "plex_id_txt";
            this.plex_id_txt.Size = new System.Drawing.Size(146, 20);
            this.plex_id_txt.TabIndex = 0;
            this.plex_id_txt.Tag = "2";
            this.plex_id_txt.TextChanged += new System.EventHandler(this.plex_id_txt_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.transfer_btn);
            this.panel3.Controls.Add(this.lon_center_txt);
            this.panel3.Controls.Add(this.lat_center_txt);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.zone_sw_lat);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.get_info_btn);
            this.panel3.Controls.Add(this.zone_ne_lon);
            this.panel3.Controls.Add(this.server_name_txt);
            this.panel3.Controls.Add(this.zone_sw_lon);
            this.panel3.Controls.Add(this.save_img_btn);
            this.panel3.Controls.Add(this.zone_ne_lat);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(343, 173);
            this.panel3.TabIndex = 52;
            // 
            // zone_sw_lat
            // 
            this.zone_sw_lat.Location = new System.Drawing.Point(12, 70);
            this.zone_sw_lat.Name = "zone_sw_lat";
            this.zone_sw_lat.ReadOnly = true;
            this.zone_sw_lat.Size = new System.Drawing.Size(82, 20);
            this.zone_sw_lat.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 13);
            this.label13.TabIndex = 50;
            this.label13.Text = "Имя файла на сервере";
            // 
            // get_info_btn
            // 
            this.get_info_btn.Enabled = false;
            this.get_info_btn.Location = new System.Drawing.Point(240, 26);
            this.get_info_btn.Margin = new System.Windows.Forms.Padding(0);
            this.get_info_btn.Name = "get_info_btn";
            this.get_info_btn.Size = new System.Drawing.Size(93, 36);
            this.get_info_btn.TabIndex = 1;
            this.get_info_btn.Text = "Получить изображение";
            this.get_info_btn.UseVisualStyleBackColor = true;
            this.get_info_btn.Click += new System.EventHandler(this.get_info_btn_Click);
            // 
            // zone_ne_lon
            // 
            this.zone_ne_lon.Location = new System.Drawing.Point(117, 109);
            this.zone_ne_lon.Name = "zone_ne_lon";
            this.zone_ne_lon.ReadOnly = true;
            this.zone_ne_lon.Size = new System.Drawing.Size(82, 20);
            this.zone_ne_lon.TabIndex = 16;
            // 
            // zone_sw_lon
            // 
            this.zone_sw_lon.Location = new System.Drawing.Point(117, 70);
            this.zone_sw_lon.Name = "zone_sw_lon";
            this.zone_sw_lon.ReadOnly = true;
            this.zone_sw_lon.Size = new System.Drawing.Size(82, 20);
            this.zone_sw_lon.TabIndex = 14;
            // 
            // save_img_btn
            // 
            this.save_img_btn.Enabled = false;
            this.save_img_btn.Location = new System.Drawing.Point(240, 81);
            this.save_img_btn.Name = "save_img_btn";
            this.save_img_btn.Size = new System.Drawing.Size(93, 36);
            this.save_img_btn.TabIndex = 2;
            this.save_img_btn.Text = "Сохранить изображение";
            this.save_img_btn.UseVisualStyleBackColor = true;
            this.save_img_btn.Click += new System.EventHandler(this.save_img_btn_Click);
            // 
            // zone_ne_lat
            // 
            this.zone_ne_lat.Location = new System.Drawing.Point(12, 108);
            this.zone_ne_lat.Name = "zone_ne_lat";
            this.zone_ne_lat.ReadOnly = true;
            this.zone_ne_lat.Size = new System.Drawing.Size(83, 20);
            this.zone_ne_lat.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "SW широта";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(115, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "NE долгота";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "NE широта";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(115, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "SW долгота";
            // 
            // out_img
            // 
            this.out_img.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.out_img.ErrorImage = ((System.Drawing.Image)(resources.GetObject("out_img.ErrorImage")));
            this.out_img.Location = new System.Drawing.Point(0, 202);
            this.out_img.Name = "out_img";
            this.out_img.Size = new System.Drawing.Size(343, 343);
            this.out_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.out_img.TabIndex = 53;
            this.out_img.TabStop = false;
            // 
            // lon_center_txt
            // 
            this.lon_center_txt.Location = new System.Drawing.Point(117, 151);
            this.lon_center_txt.Name = "lon_center_txt";
            this.lon_center_txt.ReadOnly = true;
            this.lon_center_txt.Size = new System.Drawing.Size(82, 20);
            this.lon_center_txt.TabIndex = 52;
            // 
            // lat_center_txt
            // 
            this.lat_center_txt.Location = new System.Drawing.Point(12, 150);
            this.lat_center_txt.Name = "lat_center_txt";
            this.lat_center_txt.ReadOnly = true;
            this.lat_center_txt.Size = new System.Drawing.Size(83, 20);
            this.lat_center_txt.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Долгота центра";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Широта центра";
            // 
            // transfer_btn
            // 
            this.transfer_btn.Location = new System.Drawing.Point(240, 135);
            this.transfer_btn.Name = "transfer_btn";
            this.transfer_btn.Size = new System.Drawing.Size(93, 35);
            this.transfer_btn.TabIndex = 3;
            this.transfer_btn.Text = "Передать в размещение";
            this.transfer_btn.UseVisualStyleBackColor = true;
            this.transfer_btn.Click += new System.EventHandler(this.transfer_btn_Click);
            // 
            // ImageGetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 567);
            this.Controls.Add(this.out_img);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(359, 606);
            this.Name = "ImageGetter";
            this.Text = "Получение изображения";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.out_img)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_strip;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox plex_id_txt;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox zone_sw_lat;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button get_info_btn;
        private System.Windows.Forms.TextBox zone_ne_lon;
        private System.Windows.Forms.TextBox server_name_txt;
        private System.Windows.Forms.TextBox zone_sw_lon;
        private System.Windows.Forms.Button save_img_btn;
        private System.Windows.Forms.TextBox zone_ne_lat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox out_img;
        private System.Windows.Forms.Button transfer_btn;
        private System.Windows.Forms.TextBox lon_center_txt;
        private System.Windows.Forms.TextBox lat_center_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}


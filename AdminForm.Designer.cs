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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.multi_check = new System.Windows.Forms.CheckBox();
            this.last_multi_download = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_download_multi = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.log_box = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sec_mult_gen_check = new System.Windows.Forms.CheckBox();
            this.first_mult_gen_check = new System.Windows.Forms.CheckBox();
            this.start_generator_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tower_check = new System.Windows.Forms.CheckBox();
            this.dt_time_actual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dwnld_base_btn = new System.Windows.Forms.Button();
            this.current_time_twr_txt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.get_act_date = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label5);
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
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(206, 46);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(205, 20);
            this.textBox2.TabIndex = 9;
            this.textBox2.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(206, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Дата и время последней генерации";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(406, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = ".csv файлы раскодировки должны находится в папке с исполняемым файлом";
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
            // current_time_twr_txt
            // 
            this.current_time_twr_txt.Enabled = false;
            this.current_time_twr_txt.Location = new System.Drawing.Point(169, 75);
            this.current_time_twr_txt.Name = "current_time_twr_txt";
            this.current_time_twr_txt.ReadOnly = true;
            this.current_time_twr_txt.Size = new System.Drawing.Size(199, 20);
            this.current_time_twr_txt.TabIndex = 6;
            this.current_time_twr_txt.TabStop = false;
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
            // get_act_date
            // 
            this.get_act_date.Location = new System.Drawing.Point(374, 71);
            this.get_act_date.Name = "get_act_date";
            this.get_act_date.Size = new System.Drawing.Size(38, 26);
            this.get_act_date.TabIndex = 8;
            this.get_act_date.Text = "о";
            this.get_act_date.UseVisualStyleBackColor = true;
            this.get_act_date.Click += new System.EventHandler(this.get_act_date_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 634);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.log_box);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(457, 596);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
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
    }
}
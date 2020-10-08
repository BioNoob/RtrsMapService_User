namespace RtrsMapService_User
{
    partial class RequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestForm));
            this.request_lbl = new System.Windows.Forms.Label();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.txt_input = new System.Windows.Forms.TextBox();
            this.input_req_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // request_lbl
            // 
            this.request_lbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.request_lbl.Location = new System.Drawing.Point(12, 9);
            this.request_lbl.Name = "request_lbl";
            this.request_lbl.Size = new System.Drawing.Size(381, 152);
            this.request_lbl.TabIndex = 0;
            this.request_lbl.Text = "label1";
            this.request_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(237, 164);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 1;
            this.ok_btn.Text = "ОК";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(318, 164);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 2;
            this.cancel_btn.Text = "Отмена";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // txt_input
            // 
            this.txt_input.Location = new System.Drawing.Point(103, 166);
            this.txt_input.Name = "txt_input";
            this.txt_input.Size = new System.Drawing.Size(128, 20);
            this.txt_input.TabIndex = 3;
            // 
            // input_req_lbl
            // 
            this.input_req_lbl.Location = new System.Drawing.Point(12, 166);
            this.input_req_lbl.Name = "input_req_lbl";
            this.input_req_lbl.Size = new System.Drawing.Size(85, 20);
            this.input_req_lbl.TabIndex = 4;
            this.input_req_lbl.Text = "label1";
            // 
            // RequestForm
            // 
            this.AcceptButton = this.ok_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_btn;
            this.ClientSize = new System.Drawing.Size(405, 192);
            this.ControlBox = false;
            this.Controls.Add(this.input_req_lbl);
            this.Controls.Add(this.txt_input);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.request_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RequestForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RequestForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label request_lbl;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.Label input_req_lbl;
    }
}
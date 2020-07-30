﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 
*/

namespace RtrsMapService_User
{
    public partial class ImageGetter : Form
    {
        SaveFileDialog sfd = new SaveFileDialog();
        MapBorderImg mbr = null;
        public ImageGetter()
        {
            InitializeComponent();
            mbr = new MapBorderImg();
            LoadSet();
            toolStripProgressBar1.ProgressBar.Visible = false;
            mbr.Show();
        }

        private void LoadSet()
        {
            plex_id_txt.Text = Properties.Settings.Default.current_id;
            if (Properties.Settings.Default.imgform_start_pos != new Point())
                this.Location = Properties.Settings.Default.imgform_start_pos;
            else
            {
                this.CenterToScreen();
                this.Location = new Point(Location.X - this.Width, Location.Y);
            }

            if (Properties.Settings.Default.mapform_start_pos != new Point())
                mbr.Location = Properties.Settings.Default.mapform_start_pos;
            else
                mbr.Location = new Point(this.Location.X + this.Width, this.Location.Y);
            if (Properties.Settings.Default.mapform_size != new Size())
                mbr.Size = Properties.Settings.Default.mapform_size;
        }
        private void SaveSet()
        {
            Properties.Settings.Default.current_id = plex_id_txt.Text;
            Properties.Settings.Default.imgform_start_pos = this.Location;
            Properties.Settings.Default.mapform_size = mbr.Size;
            Properties.Settings.Default.mapform_start_pos = mbr.Location;
            Properties.Settings.Default.Save();
        }

        public Image GetImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return null;
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            stream.Flush();
            stream.Close();
            client.Dispose();
            if (bitmap != null)
            {
                return bitmap;
            }
            else
                return null;
        }
        private void Placer(LoadItem li, int id)
        {
            mapobj mo = new mapobj();
            if (id == 1)
            {
                mo = li.map_trans1;
            }
            server_name_txt.Text = mo.web_name;
            zone_sw_lat.Text = mo.swx.ToString();
            zone_sw_lon.Text = mo.swy.ToString();
            zone_ne_lat.Text = mo.nex.ToString();
            zone_ne_lon.Text = mo.ney.ToString();
            lat_center_txt.Text = mo.GetCenter().X.ToString();
            lon_center_txt.Text = mo.GetCenter().Y.ToString();
            out_img.Image = GetImage(mo.map);
        }
        private void Cleaner()
        {
            //Placer(0);
            save_img_btn.Enabled = false;
            transfer_btn.Enabled = false;
            string zero = string.Empty;
            zone_sw_lat.Text = zero;
            zone_sw_lon.Text = zero;
            zone_ne_lat.Text = zero;
            zone_ne_lon.Text = zero;
            lat_center_txt.Text = zero;
            lon_center_txt.Text = zero;
            out_img.Image = null;
        }
        LoadItem li_loc = null;
        private async void get_info_btn_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.ProgressBar.Visible = true;
            this.Refresh();
            li_loc = new LoadItem();
            int id;
            if (!int.TryParse(plex_id_txt.Text, out id))
            {
                MessageBox.Show("Входная строка имела неверный формат");
                return;
            }

            //toolStripProgressBar1.Visible = true;
            //if (fl_use_base)
            //    if (dta == null)
            //Checker();
            Cleaner();
            //li = dta.GetMapInfo_plex(int.Parse(plex_id_txt.Text));
            li_loc = await LoadItem.GetMapInfoAsync(id);

            if (!string.IsNullOrEmpty(li_loc.er_string))
            {
                out_img.Image = out_img.ErrorImage;
                status_strip.Text = li_loc.er_string;
                toolStripProgressBar1.ProgressBar.Visible = false;
                return;
            }
            Placer(li_loc, 1);
            save_img_btn.Enabled = true;
            transfer_btn.Enabled = true;
            SaveSet();
            toolStripProgressBar1.ProgressBar.Visible = false;
            //SaveSet();
        }

        private void save_img_btn_Click(object sender, EventArgs e)
        {
            sfd.Filter = "png (*.png)|*.png|jpeg (*.jpeg)|*.jpeg|tiff (*.tiff)|*.tiff|bmp (*.bmp)|*.bmp";
            ImageFormat format = ImageFormat.Png;
            sfd.Title = "Сохранить изображение";
            sfd.FileName = $"ID_{plex_id_txt.Text}_plex";
            sfd.DefaultExt = "png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                out_img.Image.Save(sfd.FileName, format);
            }
            SaveSet();
        }

        private void plex_id_txt_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            string regex = @"^\d+$";
            if (Regex.IsMatch(t.Text, regex))
            {
                t.ForeColor = Color.Black;
                get_info_btn.Enabled = true;
            }
            else
            {
                t.ForeColor = Color.Red;
                get_info_btn.Enabled = false;
            }
        }

        private void transfer_btn_Click(object sender, EventArgs e)
        {
            if (mbr.IsDisposed)
                mbr = new MapBorderImg();
            mbr.Show();
            mbr.SetInputFromOutside(li_loc.map_trans1, out_img.Image);
        }

        private void ImageGetter_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void ImageGetter_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSet();
            mbr.MapBorderImg_FormClosing(mbr, new FormClosingEventArgs(CloseReason.FormOwnerClosing, false));
            mbr.Dispose();
        }
    }
}

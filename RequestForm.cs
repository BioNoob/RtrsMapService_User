using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtrsMapService_User
{
    public partial class RequestForm : Form
    {
        public string GetInput { get { return txt_input.Text; } }
        /// <summary>
        /// InputBox
        /// </summary>
        /// <param name="Text">Отображаемый текст</param>
        /// <param name="Caption">Название окна</param>
        /// <param name="InputLbl">Текст у поля ввода</param>
        public RequestForm(string Text, string Caption, string InputLbl)
        {
            InitializeComponent();
            request_lbl.Text = Text;
            this.Text = Caption;
            input_req_lbl.Text = InputLbl;
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

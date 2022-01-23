// InputForm.cs
//
// DNSRedir - custom DNS server
//
// Copyright (c) 2022 Don Mankin <don.mankin@yahoo.com>
//
// MIT License
//
// See the file LICENSE for more details, or visit <https://opensource.org/licenses/MIT>.

using System;
using System.Linq;
using System.Windows.Forms;

namespace DNSRedir
{
    public partial class InputForm : Form
    {
        private Form m_frmMain = null;
        private string m_sSelected;

        #region InputForm
        public InputForm(MainForm newForm, string sSelected)
        {

            InitializeComponent();
            this.m_frmMain = newForm;
            this.m_sSelected = sSelected;
            this.MaximizeBox = false;
            this.BackColor = MainForm.cChocolate;
            this.ForeColor = MainForm.cWhite;
            this.txtServer.BackColor = MainForm.cLightBrown;
            this.btnOk.BackColor = MainForm.cDarkBrown;
            this.btnCancel.BackColor = MainForm.cDarkBrown;
            this.txtServer.Text = sSelected;
        }
        #endregion

        #region getEditedValue
        public String getEditedValue // retrieving a value from
        {
            get
            {
                return this.txtServer.Text;
            }
        }
        #endregion

        #region btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region WndProc
        protected override void WndProc(ref Message m) // capture close message so we can save our settings
        {
            int WM_CLOSE = 0x0010;
            if (m.Msg == WM_CLOSE)
            {
                // show main form
                m_frmMain.Show();
            }
            base.WndProc(ref m);
        }
        #endregion

        #region btnOK_Click
        private void btnOK_Click(object sender, EventArgs e)
        {
            string sServer = txtServer.Text;
            int found = sServer.IndexOf(MainForm.m_sSeparator);
            if (found == -1)
            {
                MessageBox.Show("From and To servers must be separated with " + "\"" + MainForm.m_sSeparator + "\"");
                txtServer.Text = m_sSelected;  // revert
                return;
            }
            this.Close();
        }
        #endregion
    }
}

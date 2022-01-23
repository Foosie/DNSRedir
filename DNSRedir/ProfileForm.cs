// ProfileForm.cs
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
    public partial class ProfileForm : Form
    {
        private Form m_frmMain = null;

        #region constructor
        public ProfileForm(MainForm newForm)
        {
            m_frmMain = newForm;
            InitializeComponent();
            this.BackColor = MainForm.cChocolate;
            this.ForeColor = MainForm.cWhite;
            this.txtProfileName.BackColor = MainForm.cLightBrown;
            this.btnClose.BackColor = MainForm.cDarkBrown;
            this.btnOK.BackColor = MainForm.cDarkBrown;
            this.txtProfileName.ForeColor = MainForm.cWhite;
            this.btnClose.ForeColor = MainForm.cWhite;
            this.btnOK.ForeColor = MainForm.cWhite;

            // disable maximize box
            this.MaximizeBox = false;
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            m_frmMain.Show();
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
            string sProfileName = String.Concat(txtProfileName.Text.Where(c => !Char.IsWhiteSpace(c)));
            if (sProfileName.Length > 0)
            {
                // save chosen profile to DNSRedir.ini
                IniFile defaultIniFile = new IniFile(".\\DNSRedir.ini");
                defaultIniFile.IniWriteValue("DNSRedir", "txtProfile", sProfileName);

                // create a new profile.ini stub
                IniFile newIniFile = new IniFile(".\\" + sProfileName + ".ini");
                newIniFile.IniWriteValue("DNSRedir", "servers", "0");
            }
            m_frmMain.Show();
            this.Close();
        }
        #endregion
    }
}

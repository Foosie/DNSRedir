// MainForm.cs
//
// DNSRedir - custom DNS server
//
// Copyright (c) 2022 Don Mankin <don.mankin@yahoo.com>
//
// MIT License
//
// See the file LICENSE for more details, or visit <https://opensource.org/licenses/MIT>.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using DNS.Server;
using IWshRuntimeLibrary;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace DNSRedir
{
    public partial class MainForm : Form
    {
        #region declarations
        private const int WM_CLOSE = 0x0010;
        private string m_sIniFile = ".\\DNSRedir.ini";
        private string m_sDNSDLL = ".\\DNS.dll";
        public static string m_sExeDir = "";
        private IniFile m_Ini = null;
        public static string m_sSeparator = "---->";
        public static Color cChocolate = Color.FromArgb(100, 82, 72);
        public static Color cLightBrown = Color.FromArgb(150, 114, 93);
        public static Color cDarkBrown = Color.FromArgb(74, 61, 53);
        public static Color cWhite = Color.FromArgb(255, 255, 255);
        private ToolStripMenuItem[] m_mnuItems;
        #endregion

        #region constructor
        public MainForm()
        {
            InitializeComponent();
            m_Ini = new IniFile(m_sIniFile);
            this.AcceptButton = btnAdd;
            this.BackColor = cChocolate;
            this.ForeColor = cWhite;
            this.lbOutput.BackColor = cLightBrown;
            this.lbServers.BackColor = cLightBrown;
            this.txtFrom.BackColor = cLightBrown;
            this.txtTo.BackColor = cLightBrown;
            this.btnDelete.BackColor = cDarkBrown;
            this.btnEmpty.BackColor = cDarkBrown;
            this.btnStart.BackColor = cDarkBrown;
            this.btnStop.BackColor = cDarkBrown;
            this.btnAdd.BackColor = cDarkBrown;
            this.btnDeselect.BackColor = cDarkBrown;
            this.btnEdit.BackColor = cDarkBrown;
            this.mnuCreateShortcut.ForeColor = Color.White;
            this.mnuNewProfile.ForeColor = Color.White;
            this.mnuDeleteProfile.ForeColor = Color.White;
            this.mnuStrip.BackColor = cChocolate;
            this.mnuStrip.ForeColor = cWhite;
            this.mnuStrip.Renderer = new ChocolateRenderer();
            string sVersion = Application.ProductVersion;
            string[] tokens = sVersion.Split('.');
            sVersion = "v" + tokens[2] + "." + tokens[3];
            this.Text = "DNSRedir " + sVersion + " by Don Mankin";

            // set working directory to executable directory
            m_sExeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(m_sExeDir);

            // stub ini file if necessary
            if (!System.IO.File.Exists(m_sIniFile))
                m_Ini.IniWriteValue("DNSRedir", "txtProfile", "DNSRedir");

            // build the profile menu
            BuildProfileMenu();

            // now restore the selected profile
            restoreSelectedProfile();

            // get settings
            iniReadFile();
        }
        #endregion

        #region ChocolateRenderer
        private class ChocolateRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color c = cChocolate;
                using (SolidBrush brush = new SolidBrush(c))
                    e.Graphics.FillRectangle(brush, rc);
            }
        }
        #endregion

        #region iniWriteFile
        public void iniWriteFile()
        {
            if (System.IO.File.Exists(m_sIniFile))
                System.IO.File.Delete(m_sIniFile);

            m_Ini.IniWriteValue("DNSRedir", "servers", lbServers.Items.Count.ToString());
            for (int i = 0; i < lbServers.Items.Count; i++)
                m_Ini.IniWriteValue("Servers", "item" + i.ToString(), lbServers.Items[i].ToString());
            foreach (int i in lbServers.SelectedIndices)
                m_Ini.IniWriteValue("Selected", "item" + i.ToString(), lbServers.Items[i].ToString());
        }
        #endregion

        #region iniReadFile
        public void iniReadFile()
        {
            string sRet;
            int n = 0;

            // get number of servers
            if ((sRet = (m_Ini.IniReadValue("DNSRedir", "servers", "garbage").Trim())) != "garbage")
            {
                // to integer
                n = Int16.Parse(sRet);

                // add servers to listbox
                for (int i = 0; i < n; i++)
                {
                    if ((sRet = (m_Ini.IniReadValue("Servers", "item" + i.ToString(), "garbage").Trim())) != "garbage")
                        lbServers.Items.Add(sRet);
                }

                // get selected items
                for (int i = 0; i < n; i++)
                {
                    // set the selected items in listbox
                    if ((sRet = (m_Ini.IniReadValue("Selected", "item" + i.ToString(), "garbage").Trim())) != "garbage")
                    {
                        int index = lbServers.FindString(sRet);
                        if (index != -1)
                            lbServers.SetSelected(index, true);
                    }
                }
            }
        }
        #endregion

        #region WndProc
        protected override void WndProc(ref Message m) // capture close message so we can save our settings
        {
            if (m.Msg == WM_CLOSE)
            {
                iniWriteFile();
                saveSelectedProfile();
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Clicked
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ((txtFrom.Text.Length == 0) || (txtTo.Text.Length == 0))
            {
                MessageBox.Show("Add both from and to servers", "Add server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string sItem = txtFrom.Text + m_sSeparator + txtTo.Text;
            lbServers.Items.Add(sItem);
            txtFrom.Clear();
            txtTo.Clear();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            // make sure dns.dll is available
            if (!System.IO.File.Exists(m_sDNSDLL))
            {
                MessageBox.Show("DNS.dll must be in the same folder with DNSRedir.exe", "Oops - missing DNS.dll", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            // anything selected?
            if (lbServers.SelectedIndices.Count == 0)
            {
                MessageBox.Show("First select servers to redirect", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                //start dns server
                this.btnStart.Enabled = false;
                threadWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                UpdatelbOutput("========EXCEPTION=======");
                UpdatelbOutput(ex.ToString());
                UpdatelbOutput("========EXCEPTION=======");
            }
        }
        private void btnEmpty_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Empty List", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                lbServers.Items.Clear();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((null == lbServers.SelectedItem) || (lbServers.SelectedIndices.Count != 1))
            {
                MessageBox.Show("Please select one item", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete selected items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                lbServers.Items.Remove(lbServers.SelectedItem);
        }
        private void btnDeselect_Click(object sender, EventArgs e)
        {
            lbServers.ClearSelected();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if ((null == lbServers.SelectedItem) || (lbServers.SelectedIndices.Count != 1))
            {
                MessageBox.Show("Please select one item", "Edit item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = lbServers.SelectedIndex;
            InputForm frmInput = new InputForm(this, lbServers.SelectedItem.ToString());
            frmInput.ShowDialog(this);
            lbServers.Items.RemoveAt(index);
            lbServers.Items.Insert(index, frmInput.getEditedValue);
            lbServers.Refresh();
        }
        private void mnuCreateShortcut_Click(object sender, EventArgs e)
        {
            CreateShortcut("DNSRedir", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Assembly.GetExecutingAssembly().Location);
            MessageBox.Show("Shortcut created", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void mnuNewProfile_Click(object sender, EventArgs e)
        {
            ProfileForm m_frmProfile = new ProfileForm(this);
            m_frmProfile.ShowDialog(this);

            // now restart the application
            Application.Restart();
        }
        private void mnuDeleteProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.RestoreDirectory = true; // make sure directory is set to executable path
            openDialog.AddExtension = true;
            openDialog.CheckFileExists = true;
            openDialog.DefaultExt = "ini";
            openDialog.InitialDirectory = m_sExeDir;
            openDialog.Multiselect = false;
            openDialog.Title = "Select a profile (current profile excluded)";
            openDialog.Filter = "Profiles (*.ini;) | *.ini;";
            DialogResult drRet = DialogResult.Cancel;
            if ((drRet = openDialog.ShowDialog()) == DialogResult.OK)
            {
                while (Path.GetDirectoryName(openDialog.FileName) != Path.Combine(Path.GetDirectoryName(Application.StartupPath), m_sExeDir))
                {
                    MessageBox.Show("Please select a profile which is in the default folder", "Wrong folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    drRet = openDialog.ShowDialog();
                }
                string sFilename = openDialog.FileName;
                string sProfilename = Path.GetFileNameWithoutExtension(sFilename);
                if (sProfilename == "DNSRedir")
                    MessageBox.Show("You cannot delete the default profile", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    // determine the selected profile
                    for (int i = 0; i < m_mnuItems.Length; i++)
                    {
                        if ((m_mnuItems[i].Checked) && (m_mnuItems[i].ToString() == sProfilename))
                        {
                            MessageBox.Show("You cannot delete the current profile", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    drRet = MessageBox.Show("Delete profile " + sProfilename + ".\n\n Are you sure?", "Delete Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drRet == DialogResult.Yes)
                    {
                        System.IO.File.Delete(sFilename);
                        Application.Restart();
                    }
                }
            }
        }
        private void mnuInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program creates a DNS server that can redirect IP traffic from one domain to another.  It can be used to allow a Minecraft Bedrock server to support consoles.  Other uses can be content filtering for children by blocking a computer or phone app’s destination address. Point your device's preferred name server to the ipaddress running this application.  Point it's secondary server to your router's gateway address, 1.1.1.1, 8.8.8.8, or wherever is appropriate. By setting up a secondary, if this application is not running, your device will still work properly.");
        }
        private void mnuLocalIP_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetLocalIPAddress());
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            // restarting stops threads
            Application.Restart();
        }
        #endregion

        #region isProperIp
        // is ip properly formatted?
        private bool isProperIp(string ip)
        {
            IPAddress IP;
            return IPAddress.TryParse(ip, out IP);
        }
        #endregion

        #region GetHostIPAddress
        private string GetHostIPAddress(string hostName)
        {
            string sIpAddress = "ERR";

            // if the hostname is really an ipaddress, just return it
            //if (isProperIp(hostName))
            //    return hostName;

            try
            {
                IPHostEntry IPEntry = Dns.GetHostEntry(hostName);
                foreach (IPAddress ip in IPEntry.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        sIpAddress = ip.ToString();
                        break; // return first ipadress
                    }
                }
            }
            catch (Exception ex)
            {
                UpdatelbOutput("==================== EXCEPTION ====================");
                UpdatelbOutput("Cannot resolve host \"" + hostName + "\" to IPv4 ipAddress");
                UpdatelbOutput("");
                Environment.Exit(1);
            }
            if (sIpAddress == "ERR")
            {
                UpdatelbOutput("==================== EXCEPTION ====================");
                UpdatelbOutput("Cannot resolve host \"" + hostName + "\" to IPv4 ipAddress");
                UpdatelbOutput("");
            }
            return sIpAddress;
        }
        #endregion

        #region GetReverseDNS
        private string GetReverseDNS(string sIpAddress)
        {
            string hostName = "ERR";
            try
            {
                IPHostEntry IPEntry = Dns.GetHostEntry(sIpAddress);
                hostName = IPEntry.HostName;
            }
            catch (Exception ex)
            {
                UpdatelbOutput("==================== EXCEPTION ====================");
                UpdatelbOutput("Cannot get hostname from  \"" + sIpAddress + "\"");
                UpdatelbOutput("==================== EXCEPTION ====================");
                Environment.Exit(1);
            }
            if (sIpAddress == "ERR")
            {
                UpdatelbOutput("==================== EXCEPTION ====================");
                UpdatelbOutput("Cannot get hostname from  \"" + sIpAddress + "\"");
                UpdatelbOutput("==================== EXCEPTION ====================");
            }
            return hostName;
        }
        #endregion

        #region GetLocalIPAddress
        public string GetLocalIPAddress()
        {
            string sIpAddress = "ERR";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        sIpAddress = ip.ToString();
                        break; // return first ipadress
                    }
                }
                if (sIpAddress.Length == 0)
                {
                    UpdatelbOutput("=============== EXCEPTION =================");
                    UpdatelbOutput("Cannot resolve local host to IPv4 ipAddress");
                    UpdatelbOutput("");
                    Debug.WriteLine("DNSRedir: Cannot resolve local host to IPv4 ipAddress");
                }
            }
            catch (Exception ex)
            {
                UpdatelbOutput("=============== EXCEPTION =================");
                UpdatelbOutput("Cannot resolve local host to IPv4 ipAddress");
                UpdatelbOutput("");
                Debug.WriteLine("DNSRedir: Cannot resolve local host to IPv4 ipAddress");
            }
            return sIpAddress;
        }
        #endregion

        #region GetServerList
        private List<string> GetServerList()
        {
            List<string> serverList = new List<string>();
            string sFromServer = "";
            string sToServer = "";
            string sLine = "";
            int iLeft = 0;

            // for each selected server ip in listbox
            foreach (int i in lbServers.SelectedIndices)
            {
                sLine = lbServers.Items[i].ToString();
                iLeft = sLine.IndexOf(m_sSeparator);
                if (iLeft != -1) // found?
                {
                    // parse server names from listbox line
                    sFromServer = sLine.Substring(0, iLeft);
                    sToServer = sLine.Substring(iLeft + m_sSeparator.Length);
                    serverList.Add(sFromServer + m_sSeparator + sToServer);
                }
            }

            return serverList;
        }
        #endregion

        #region CreateShortcut
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = "DNSRedir";
            shortcut.TargetPath = targetFileLocation;
            shortcut.Save();
        }
        #endregion

        #region MenuItemClickHandler
        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            // clear listboxes
            lbServers.Items.Clear();
            lbOutput.Items.Clear();

            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            for (int i = 0; i < m_mnuItems.Length; i++)
            {
                m_mnuItems[i].Checked = false;
                if (m_mnuItems[i] == clickedItem)
                    m_mnuItems[i].Checked = true;
            }

            // new ini file
            m_sIniFile = ".\\" + clickedItem + ".ini";
            m_Ini = new IniFile(m_sIniFile);
            iniReadFile();
        }
        #endregion

        #region BuildProfileMenu
        private void BuildProfileMenu()
        {
            string[] filePaths = System.IO.Directory.GetFiles(m_sExeDir, "*.ini");
            m_mnuItems = null; // and dispose of any sub-items

            m_mnuItems = new ToolStripMenuItem[filePaths.Length];
            string fname;

            for (int i = 0; i < m_mnuItems.Length; i++)
            {
                fname = System.IO.Path.GetFileNameWithoutExtension(filePaths[i]);
                m_mnuItems[i] = new ToolStripMenuItem();
                m_mnuItems[i].BackColor = cChocolate;
                m_mnuItems[i].ForeColor = Color.White;
                m_mnuItems[i].Name = "mnu" + fname;
                m_mnuItems[i].Text = fname;
                m_mnuItems[i].Click += new EventHandler(MenuItemClickHandler);
            }

            mnuProfiles.DropDownItems.AddRange(m_mnuItems);
        }
        #endregion

        #region restoreSelectedProfile
        private void restoreSelectedProfile()
        {
            string sRet;
            IniFile ifile = new IniFile(".\\DNSRedir.ini");
            sRet = m_Ini.IniReadValue("DNSRedir", "txtProfile", "garbage").Trim();
            for (int i = 0; i < m_mnuItems.Length; i++)
            {
                m_mnuItems[i].Checked = false;
                if (m_mnuItems[i].ToString() == sRet)
                {
                    m_mnuItems[i].Checked = true;
                    m_sIniFile = ".\\" + sRet + ".ini";
                    m_Ini = new IniFile(m_sIniFile);
                }
            }
        }
        #endregion

        #region saveSelectedProfile
        private void saveSelectedProfile()
        {
            bool bFound = false;
            IniFile ifile = new IniFile(".\\DNSRedir.ini");
            for (int i = 0; i < m_mnuItems.Length; i++)
            {
                if (m_mnuItems[i].Checked)
                {
                    ifile.IniWriteValue("DNSRedir", "txtProfile", m_mnuItems[i].ToString());
                    bFound = true;
                }
            }
            if (!bFound)
                ifile.IniWriteValue("DNSRedir", "txtProfile", "DNSRedir");
        }
        #endregion

        #region threadWorker
        private void threadWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            startServer(m_sSeparator);
        }
        private void threadWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //btnLaunch.Enabled = true;
            //btnBack.Enabled = true;
        }
        #endregion

        #region startServer
        private void startServer(string sSeparator)
        {
            // NOTE: It appears the source must be a domain name
            //       and the destination must be an ip address

            Debug.WriteLine("DNSRedir: Starting Server...");

            string iLocalIp = GetLocalIPAddress();
            string sFromServer = "";
            string sToAddress = "";
            string sLine = "";
            int iLeft = 0;

            // abort if local ip not found
            if ("ERR" == iLocalIp)
                return;

            // get selected server list
            List<string> serverList = invokedGetServerList();

            string hostName = Dns.GetHostName();
            MasterFile masterFile = new MasterFile();
            DnsServer server = new DnsServer(masterFile, "8.8.8.8");

            // for each selected server ip in listbox
            for (int i = 0; i < serverList.Count; i++)
            {
                sLine = serverList[i];
                iLeft = sLine.IndexOf(sSeparator);
                if (iLeft != -1) // found?
                {
                    // parse server names from listbox line
                    sFromServer = sLine.Substring(0, iLeft);
                    sToAddress = sLine.Substring(iLeft + sSeparator.Length);

                    // convert from ipaddress to hostname if necessary
                    if (isProperIp(sFromServer))
                    {
                        string sNewFromServer = GetReverseDNS(sFromServer);
                        if ("ERR" == sNewFromServer)  // next listbox line
                            continue;
                        if (sFromServer != sNewFromServer)
                        {
                            UpdatelbOutput("Resolved " + sFromServer + " to " + sNewFromServer);
                            sFromServer = sNewFromServer;
                        }
                    }

                    // convert from hostname to ipaddress if necessary
                    if (!isProperIp(sToAddress))
                    {
                        string sNewToAddress = GetHostIPAddress(sToAddress);
                        if ("ERR" == sNewToAddress)
                            continue;  // invalid ipaddresses continue with next in list
                        if (sToAddress != sNewToAddress)
                        {
                            UpdatelbOutput("Resolved " + sToAddress + " to " + sNewToAddress);
                            sToAddress = sNewToAddress;
                        }
                    }
                    UpdatelbOutput("Redirecting " + sFromServer + " to " + sToAddress);
                    UpdatelbOutput("+++++");
                    masterFile.AddIPAddressResourceRecord(sFromServer, sToAddress);
                }
                else
                    Debug.WriteLine("DNSRedir: Separator not found");
            }

            server.Requested += (sender, e) => process_ServerRequested(sender, e);
            server.Responded += (sender, e) => process_ServerResponded(sender, e);
            server.Errored += (sender, e) => process_ServerErrored(sender, e);

            UpdatelbOutput("");
            UpdatelbOutput("Custom DNS server is up.");
            UpdatelbOutput("");
            UpdatelbOutput("Set your device's Preferred DNS Server to " + GetLocalIPAddress());
            UpdatelbOutput("Set your device's Alternate DNS Server to 8.8.8.8, 1.1.1.1, or 9.9.9.9");
            UpdatelbOutput("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            UpdatelbOutput("");

            server.Listen();
        }

        void process_ServerRequested(object sender, DnsServer.RequestedEventArgs e)
        {
            if (null != e.Request)
            {
                UpdatelbOutput(e.Request.ToString());
            }
        }
        void process_ServerResponded(object sender, DnsServer.RespondedEventArgs e)
        {
            if ((null != e.Request && (null != e.Request)))
            {
                UpdatelbOutput(e.Request.ToString() + " => " + e.Response.ToString());
            }
        }
        void process_ServerErrored(object sender, DnsServer.ErroredEventArgs e)
        {
            if (null != e.Exception.Message)
            {
                UpdatelbOutput(e.Exception.Message);
            }
        }
        private void UpdatelbOutput(string lineOutput)
        {
            if (lbOutput.InvokeRequired)
            {
                lbOutput.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    lbOutputDoUpdate(lineOutput);
                });
            }
            else
            {
                lbOutputDoUpdate(lineOutput);
            }
        }
        private void lbOutputDoUpdate(string lineOutput)
        {
            lbOutput.Items.Add(lineOutput);
            lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
        }
        private List<string> invokedGetServerList()
        {
            List<string> serverList = null;

            if (lbOutput.InvokeRequired)
            {
                // Running on the UI thread
                lbOutput.Invoke((MethodInvoker)delegate {
                    serverList = GetServerList();
                });
            }
            else
            {
                serverList = GetServerList();
            }
            return serverList;
        }
        private void ClearlbOutput()
        {
            if (lbOutput.InvokeRequired)
            {
                lbOutput.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    lbOutput.Items.Clear();
                });
            }
            else
            {
                lbOutput.Items.Clear();
            }
        }

        #endregion
    }
}

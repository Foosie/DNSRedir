// MainForm.Designer.cs
//
// DNSRedir - custom DNS server
//
// Copyright (c) 2022 Don Mankin <don.mankin@yahoo.com>
//
// MIT License
//
// See the file LICENSE for more details, or visit <https://opensource.org/licenses/MIT>.

namespace DNSRedir
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDeselect = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEmpty = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbServers = new System.Windows.Forms.ListBox();
            this.gb2 = new System.Windows.Forms.GroupBox();
            this.lblToServer = new System.Windows.Forms.Label();
            this.lblFromServer = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.lbOutput = new System.Windows.Forms.ListBox();
            this.threadWorker = new System.ComponentModel.BackgroundWorker();
            this.mnuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuProfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreateShortcut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLocalIP = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStop = new System.Windows.Forms.Button();
            this.gb1.SuspendLayout();
            this.gb2.SuspendLayout();
            this.mnuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.btnStop);
            this.gb1.Controls.Add(this.btnEdit);
            this.gb1.Controls.Add(this.btnDeselect);
            this.gb1.Controls.Add(this.btnDelete);
            this.gb1.Controls.Add(this.btnEmpty);
            this.gb1.Controls.Add(this.btnStart);
            this.gb1.Controls.Add(this.lbServers);
            this.gb1.Location = new System.Drawing.Point(12, 22);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(587, 242);
            this.gb1.TabIndex = 0;
            this.gb1.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(489, 53);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 31);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnDeselect.ForeColor = System.Drawing.Color.White;
            this.btnDeselect.Location = new System.Drawing.Point(489, 16);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(87, 31);
            this.btnDeselect.TabIndex = 3;
            this.btnDeselect.Text = "Deselect";
            this.btnDeselect.UseVisualStyleBackColor = false;
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(489, 90);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 31);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEmpty
            // 
            this.btnEmpty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnEmpty.ForeColor = System.Drawing.Color.White;
            this.btnEmpty.Location = new System.Drawing.Point(489, 127);
            this.btnEmpty.Name = "btnEmpty";
            this.btnEmpty.Size = new System.Drawing.Size(87, 31);
            this.btnEmpty.TabIndex = 6;
            this.btnEmpty.Text = "Empty";
            this.btnEmpty.UseVisualStyleBackColor = false;
            this.btnEmpty.Click += new System.EventHandler(this.btnEmpty_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(489, 164);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(87, 31);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbServers
            // 
            this.lbServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbServers.ForeColor = System.Drawing.Color.White;
            this.lbServers.FormattingEnabled = true;
            this.lbServers.ItemHeight = 16;
            this.lbServers.Location = new System.Drawing.Point(12, 18);
            this.lbServers.Name = "lbServers";
            this.lbServers.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbServers.Size = new System.Drawing.Size(467, 212);
            this.lbServers.TabIndex = 2;
            // 
            // gb2
            // 
            this.gb2.Controls.Add(this.lblToServer);
            this.gb2.Controls.Add(this.lblFromServer);
            this.gb2.Controls.Add(this.txtTo);
            this.gb2.Controls.Add(this.btnAdd);
            this.gb2.Controls.Add(this.txtFrom);
            this.gb2.Location = new System.Drawing.Point(12, 267);
            this.gb2.Name = "gb2";
            this.gb2.Size = new System.Drawing.Size(587, 82);
            this.gb2.TabIndex = 9;
            this.gb2.TabStop = false;
            // 
            // lblToServer
            // 
            this.lblToServer.AutoSize = true;
            this.lblToServer.Location = new System.Drawing.Point(249, 18);
            this.lblToServer.Name = "lblToServer";
            this.lblToServer.Size = new System.Drawing.Size(69, 17);
            this.lblToServer.TabIndex = 12;
            this.lblToServer.Text = "To server";
            // 
            // lblFromServer
            // 
            this.lblFromServer.AutoSize = true;
            this.lblFromServer.Location = new System.Drawing.Point(12, 18);
            this.lblFromServer.Name = "lblFromServer";
            this.lblFromServer.Size = new System.Drawing.Size(84, 17);
            this.lblFromServer.TabIndex = 10;
            this.lblFromServer.Text = "From server";
            // 
            // txtTo
            // 
            this.txtTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtTo.ForeColor = System.Drawing.Color.White;
            this.txtTo.Location = new System.Drawing.Point(249, 43);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(230, 22);
            this.txtTo.TabIndex = 13;
            this.txtTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(489, 39);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 31);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtFrom
            // 
            this.txtFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtFrom.ForeColor = System.Drawing.Color.White;
            this.txtFrom.Location = new System.Drawing.Point(12, 43);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(230, 22);
            this.txtFrom.TabIndex = 11;
            this.txtFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbOutput
            // 
            this.lbOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbOutput.ForeColor = System.Drawing.Color.White;
            this.lbOutput.FormattingEnabled = true;
            this.lbOutput.HorizontalScrollbar = true;
            this.lbOutput.ItemHeight = 16;
            this.lbOutput.Location = new System.Drawing.Point(13, 362);
            this.lbOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(586, 164);
            this.lbOutput.TabIndex = 15;
            // 
            // threadWorker
            // 
            this.threadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadWorker_DoWork);
            this.threadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadWorker_RunWorkerCompleted);
            // 
            // mnuStrip
            // 
            this.mnuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProfiles,
            this.mnuCreateShortcut,
            this.mnuLocalIP,
            this.mnuInfo});
            this.mnuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuStrip.Name = "mnuStrip";
            this.mnuStrip.Size = new System.Drawing.Size(610, 28);
            this.mnuStrip.TabIndex = 15;
            // 
            // mnuProfiles
            // 
            this.mnuProfiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewProfile,
            this.mnuDeleteProfile});
            this.mnuProfiles.Name = "mnuProfiles";
            this.mnuProfiles.Size = new System.Drawing.Size(64, 24);
            this.mnuProfiles.Text = "Profile";
            // 
            // mnuNewProfile
            // 
            this.mnuNewProfile.Name = "mnuNewProfile";
            this.mnuNewProfile.Size = new System.Drawing.Size(140, 26);
            this.mnuNewProfile.Text = "-New-";
            this.mnuNewProfile.Click += new System.EventHandler(this.mnuNewProfile_Click);
            // 
            // mnuDeleteProfile
            // 
            this.mnuDeleteProfile.Name = "mnuDeleteProfile";
            this.mnuDeleteProfile.Size = new System.Drawing.Size(140, 26);
            this.mnuDeleteProfile.Text = "-Delete-";
            this.mnuDeleteProfile.Click += new System.EventHandler(this.mnuDeleteProfile_Click);
            // 
            // mnuCreateShortcut
            // 
            this.mnuCreateShortcut.Name = "mnuCreateShortcut";
            this.mnuCreateShortcut.Size = new System.Drawing.Size(123, 24);
            this.mnuCreateShortcut.Text = "Create Shortcut";
            this.mnuCreateShortcut.Click += new System.EventHandler(this.mnuCreateShortcut_Click);
            // 
            // mnuLocalIP
            // 
            this.mnuLocalIP.Name = "mnuLocalIP";
            this.mnuLocalIP.Size = new System.Drawing.Size(72, 24);
            this.mnuLocalIP.Text = "Local IP";
            this.mnuLocalIP.Click += new System.EventHandler(this.mnuLocalIP_Click);
            // 
            // mnuInfo
            // 
            this.mnuInfo.Name = "mnuInfo";
            this.mnuInfo.Size = new System.Drawing.Size(47, 24);
            this.mnuInfo.Text = "Info";
            this.mnuInfo.Click += new System.EventHandler(this.mnuInfo_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(489, 201);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(87, 31);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(610, 537);
            this.Controls.Add(this.lbOutput);
            this.Controls.Add(this.gb2);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.mnuStrip);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DNSRedir - by Don Mankin";
            this.gb1.ResumeLayout(false);
            this.gb2.ResumeLayout(false);
            this.gb2.PerformLayout();
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEmpty;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lbServers;
        private System.Windows.Forms.GroupBox gb2;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Button btnDeselect;
        private System.Windows.Forms.ListBox lbOutput;
        private System.ComponentModel.BackgroundWorker threadWorker;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblToServer;
        private System.Windows.Forms.Label lblFromServer;
        private System.Windows.Forms.MenuStrip mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateShortcut;
        private System.Windows.Forms.ToolStripMenuItem mnuProfiles;
        private System.Windows.Forms.ToolStripMenuItem mnuNewProfile;  ///Fix
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteProfile;
        private System.Windows.Forms.ToolStripMenuItem mnuInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuLocalIP;
        private System.Windows.Forms.Button btnStop;
    }
}


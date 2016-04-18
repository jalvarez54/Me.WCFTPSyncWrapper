namespace Me.WFTPSyncWrapper
{
    partial class FormMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabPageNewSeries = new System.Windows.Forms.TabPage();
            this.richTextBoxNewSeries = new System.Windows.Forms.RichTextBox();
            this.tabPageSeedboxIni = new System.Windows.Forms.TabPage();
            this.listBoxSeedBoxIni = new System.Windows.Forms.ListBox();
            this.tabPageFTPSyncHelper = new System.Windows.Forms.TabPage();
            this.groupBoxFTPSyncOptions = new System.Windows.Forms.GroupBox();
            this.radioButtonFull = new System.Windows.Forms.RadioButton();
            this.checkBoxQuiet = new System.Windows.Forms.CheckBox();
            this.radioButtonIncremental = new System.Windows.Forms.RadioButton();
            this.checkBoxInit = new System.Windows.Forms.CheckBox();
            this.radioButtonDifferential = new System.Windows.Forms.RadioButton();
            this.buttonRunFTPSync = new System.Windows.Forms.Button();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.listBoxSettings = new System.Windows.Forms.ListBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageNewSeries.SuspendLayout();
            this.tabPageSeedboxIni.SuspendLayout();
            this.tabPageFTPSyncHelper.SuspendLayout();
            this.groupBoxFTPSyncOptions.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // tabPageNewSeries
            // 
            this.tabPageNewSeries.Controls.Add(this.richTextBoxNewSeries);
            this.tabPageNewSeries.Location = new System.Drawing.Point(4, 22);
            this.tabPageNewSeries.Name = "tabPageNewSeries";
            this.tabPageNewSeries.Size = new System.Drawing.Size(1038, 682);
            this.tabPageNewSeries.TabIndex = 2;
            this.tabPageNewSeries.Text = "New Series";
            this.tabPageNewSeries.UseVisualStyleBackColor = true;
            // 
            // richTextBoxNewSeries
            // 
            this.richTextBoxNewSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxNewSeries.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxNewSeries.Name = "richTextBoxNewSeries";
            this.richTextBoxNewSeries.Size = new System.Drawing.Size(1038, 682);
            this.richTextBoxNewSeries.TabIndex = 0;
            this.richTextBoxNewSeries.Text = "";
            // 
            // tabPageSeedboxIni
            // 
            this.tabPageSeedboxIni.Controls.Add(this.listBoxSeedBoxIni);
            this.tabPageSeedboxIni.Location = new System.Drawing.Point(4, 22);
            this.tabPageSeedboxIni.Name = "tabPageSeedboxIni";
            this.tabPageSeedboxIni.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSeedboxIni.Size = new System.Drawing.Size(1038, 682);
            this.tabPageSeedboxIni.TabIndex = 1;
            this.tabPageSeedboxIni.Text = "SeedBox.ini";
            this.tabPageSeedboxIni.UseVisualStyleBackColor = true;
            // 
            // listBoxSeedBoxIni
            // 
            this.listBoxSeedBoxIni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSeedBoxIni.FormattingEnabled = true;
            this.listBoxSeedBoxIni.Location = new System.Drawing.Point(3, 3);
            this.listBoxSeedBoxIni.Name = "listBoxSeedBoxIni";
            this.listBoxSeedBoxIni.Size = new System.Drawing.Size(1032, 676);
            this.listBoxSeedBoxIni.TabIndex = 0;
            // 
            // tabPageFTPSyncHelper
            // 
            this.tabPageFTPSyncHelper.Controls.Add(this.groupBoxFTPSyncOptions);
            this.tabPageFTPSyncHelper.Controls.Add(this.buttonRunFTPSync);
            this.tabPageFTPSyncHelper.Controls.Add(this.textBoxLogs);
            this.tabPageFTPSyncHelper.Controls.Add(this.listBoxSettings);
            this.tabPageFTPSyncHelper.Location = new System.Drawing.Point(4, 22);
            this.tabPageFTPSyncHelper.Name = "tabPageFTPSyncHelper";
            this.tabPageFTPSyncHelper.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFTPSyncHelper.Size = new System.Drawing.Size(1038, 682);
            this.tabPageFTPSyncHelper.TabIndex = 0;
            this.tabPageFTPSyncHelper.Text = "FTPSyncHelper";
            this.tabPageFTPSyncHelper.UseVisualStyleBackColor = true;
            // 
            // groupBoxFTPSyncOptions
            // 
            this.groupBoxFTPSyncOptions.Controls.Add(this.radioButtonFull);
            this.groupBoxFTPSyncOptions.Controls.Add(this.checkBoxQuiet);
            this.groupBoxFTPSyncOptions.Controls.Add(this.radioButtonIncremental);
            this.groupBoxFTPSyncOptions.Controls.Add(this.checkBoxInit);
            this.groupBoxFTPSyncOptions.Controls.Add(this.radioButtonDifferential);
            this.groupBoxFTPSyncOptions.Location = new System.Drawing.Point(8, 182);
            this.groupBoxFTPSyncOptions.Name = "groupBoxFTPSyncOptions";
            this.groupBoxFTPSyncOptions.Size = new System.Drawing.Size(540, 47);
            this.groupBoxFTPSyncOptions.TabIndex = 9;
            this.groupBoxFTPSyncOptions.TabStop = false;
            this.groupBoxFTPSyncOptions.Text = "FTPSync options";
            // 
            // radioButtonFull
            // 
            this.radioButtonFull.AutoSize = true;
            this.radioButtonFull.Location = new System.Drawing.Point(6, 19);
            this.radioButtonFull.Name = "radioButtonFull";
            this.radioButtonFull.Size = new System.Drawing.Size(56, 17);
            this.radioButtonFull.TabIndex = 5;
            this.radioButtonFull.TabStop = true;
            this.radioButtonFull.Text = "/FULL";
            this.radioButtonFull.UseVisualStyleBackColor = true;
            // 
            // checkBoxQuiet
            // 
            this.checkBoxQuiet.AutoSize = true;
            this.checkBoxQuiet.Location = new System.Drawing.Point(470, 16);
            this.checkBoxQuiet.Name = "checkBoxQuiet";
            this.checkBoxQuiet.Size = new System.Drawing.Size(64, 17);
            this.checkBoxQuiet.TabIndex = 8;
            this.checkBoxQuiet.Text = "/QUIET";
            this.checkBoxQuiet.UseVisualStyleBackColor = true;
            // 
            // radioButtonIncremental
            // 
            this.radioButtonIncremental.AutoSize = true;
            this.radioButtonIncremental.Location = new System.Drawing.Point(68, 19);
            this.radioButtonIncremental.Name = "radioButtonIncremental";
            this.radioButtonIncremental.Size = new System.Drawing.Size(107, 17);
            this.radioButtonIncremental.TabIndex = 6;
            this.radioButtonIncremental.TabStop = true;
            this.radioButtonIncremental.Text = "/INCREMENTAL";
            this.radioButtonIncremental.UseVisualStyleBackColor = true;
            this.radioButtonIncremental.CheckedChanged += new System.EventHandler(this.radioButtonIncrementalDifferential_CheckedChanged);
            // 
            // checkBoxInit
            // 
            this.checkBoxInit.AutoSize = true;
            this.checkBoxInit.Location = new System.Drawing.Point(412, 16);
            this.checkBoxInit.Name = "checkBoxInit";
            this.checkBoxInit.Size = new System.Drawing.Size(52, 17);
            this.checkBoxInit.TabIndex = 4;
            this.checkBoxInit.Text = "/INIT";
            this.checkBoxInit.UseVisualStyleBackColor = true;
            this.checkBoxInit.CheckedChanged += new System.EventHandler(this.checkBoxInit_CheckedChanged);
            // 
            // radioButtonDifferential
            // 
            this.radioButtonDifferential.AutoSize = true;
            this.radioButtonDifferential.Location = new System.Drawing.Point(181, 19);
            this.radioButtonDifferential.Name = "radioButtonDifferential";
            this.radioButtonDifferential.Size = new System.Drawing.Size(106, 17);
            this.radioButtonDifferential.TabIndex = 7;
            this.radioButtonDifferential.TabStop = true;
            this.radioButtonDifferential.Text = "/DIFFERENTIAL";
            this.radioButtonDifferential.UseVisualStyleBackColor = true;
            this.radioButtonDifferential.CheckedChanged += new System.EventHandler(this.radioButtonIncrementalDifferential_CheckedChanged);
            // 
            // buttonRunFTPSync
            // 
            this.buttonRunFTPSync.Location = new System.Drawing.Point(554, 190);
            this.buttonRunFTPSync.Name = "buttonRunFTPSync";
            this.buttonRunFTPSync.Size = new System.Drawing.Size(476, 39);
            this.buttonRunFTPSync.TabIndex = 3;
            this.buttonRunFTPSync.Text = "Run FTPSync";
            this.buttonRunFTPSync.UseVisualStyleBackColor = true;
            this.buttonRunFTPSync.Click += new System.EventHandler(this.buttonRunFTPSync_Click);
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogs.Location = new System.Drawing.Point(3, 235);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogs.Size = new System.Drawing.Size(1032, 444);
            this.textBoxLogs.TabIndex = 0;
            // 
            // listBoxSettings
            // 
            this.listBoxSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBoxSettings.FormattingEnabled = true;
            this.listBoxSettings.Location = new System.Drawing.Point(3, 3);
            this.listBoxSettings.Name = "listBoxSettings";
            this.listBoxSettings.Size = new System.Drawing.Size(1032, 173);
            this.listBoxSettings.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageFTPSyncHelper);
            this.tabControl.Controls.Add(this.tabPageSeedboxIni);
            this.tabControl.Controls.Add(this.tabPageNewSeries);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1046, 708);
            this.tabControl.TabIndex = 4;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 708);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabPageNewSeries.ResumeLayout(false);
            this.tabPageSeedboxIni.ResumeLayout(false);
            this.tabPageFTPSyncHelper.ResumeLayout(false);
            this.tabPageFTPSyncHelper.PerformLayout();
            this.groupBoxFTPSyncOptions.ResumeLayout(false);
            this.groupBoxFTPSyncOptions.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TabPage tabPageNewSeries;
        private System.Windows.Forms.RichTextBox richTextBoxNewSeries;
        private System.Windows.Forms.TabPage tabPageSeedboxIni;
        private System.Windows.Forms.ListBox listBoxSeedBoxIni;
        private System.Windows.Forms.TabPage tabPageFTPSyncHelper;
        private System.Windows.Forms.Button buttonRunFTPSync;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.ListBox listBoxSettings;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.RadioButton radioButtonDifferential;
        private System.Windows.Forms.RadioButton radioButtonIncremental;
        private System.Windows.Forms.RadioButton radioButtonFull;
        private System.Windows.Forms.CheckBox checkBoxInit;
        private System.Windows.Forms.GroupBox groupBoxFTPSyncOptions;
        private System.Windows.Forms.CheckBox checkBoxQuiet;
    }
}


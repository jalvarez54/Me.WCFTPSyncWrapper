using MadMilkman.Ini;
using Me.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Me.WFTPSyncWrapper
{
    public partial class FormMain : Form
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private  Options _options = new Options();
        private Me.WCFTPSyncWrapper.IFTPSyncWrapper _ftpSyncWrapper;

        public FormMain()
        {
            log.Info("");
            InitializeComponent();
            // redirect console to textBoxLogs
            Console.SetOut(new TextBoxWriter(this.textBoxLogs));
            log.InfoFormat(Me.Common.Resources.Begin, "WinForm");

            try
            {
                // _ftpSyncWrapper object to retreive settings and display them
                // Using dependency injection
                this._ftpSyncWrapper = new Me.WCFTPSyncWrapper.Factory().GetWCFTPSyncWrapper();
                this.buttonRunFTPSync.Enabled = true;
            }
            catch (Exception ex)
            {               
                Me.Common.Utils.DisplayInMessageBox(ex.Message);
                log.Debug(ex.Message);
                Application.Exit();
            }

            this.notifyIcon.Icon = Properties.Resources.favicon;
            this.notifyIcon.Text = Me.Common.Resources.Running;
            // TODO: ShowBalloonTip not showned
            this.notifyIcon.ShowBalloonTip(10000, Application.ProductName, this.notifyIcon.Text, ToolTipIcon.Info);
            this.notifyIcon.Visible = true;

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FileStream fs = null;

            // [10003-003]  ADD: in CFTPSyncWrapper and WFTPSyncWrapper same command line parameters than FTPSync
            // FTPSync parameters retreive values from app.settings
            this.checkBoxQuiet.Checked = this._options.Quiet = this._ftpSyncWrapper.Quiet;
            this.radioButtonFull.Checked = this._options.Full = this._ftpSyncWrapper.Full;
            this.radioButtonIncremental.Checked = this._options.Incremental = this._ftpSyncWrapper.Incremental;
            this.radioButtonDifferential.Checked = this._options.Differential = this._ftpSyncWrapper.Differential;
            this.checkBoxInit.Checked = this._options.Init = this._ftpSyncWrapper.Init;

            try
            {
                log.Info("");

                // Display settings
                List<string> settings = new List<string>();
                settings.Add("/QUIET = " + this._ftpSyncWrapper.Quiet.ToString());
                settings.Add("/FULL = " + this._ftpSyncWrapper.Full.ToString());
                settings.Add("/INIT = " + this._ftpSyncWrapper.Init.ToString());
                settings.Add("/DIFFERENTIAL = " + this._ftpSyncWrapper.Differential.ToString());
                settings.Add("/INCREMENTAL = " + this._ftpSyncWrapper.Incremental.ToString());
                settings.Add("SERIES PATH = " + this._ftpSyncWrapper.SeriesPath);
                settings.Add("DOWNLOADS PATH = " + this._ftpSyncWrapper.DownloadsPath);
                settings.Add("DOWNLOADED LIST PATH = " + this._ftpSyncWrapper.DownloadedListFileLog);
                settings.Add("DUPLICATED PATH = " + this._ftpSyncWrapper.DuplicatedPath);
                listBoxSettings.DataSource = settings;

                // Display seedbox.ini
                IniFile seedboxIniFile = new IniFile();
                string iniFileName = System.Configuration.ConfigurationManager.AppSettings["iniFileName"];
                string iniPath = System.Configuration.ConfigurationManager.AppSettings["iniPath"];
                string iniFullPath = string.Format("{0}/{1}", iniPath, iniFileName);
                seedboxIniFile.Load(iniFullPath);
                foreach (var section in seedboxIniFile.Sections.ToList())
                {
                    this.listBoxSeedBoxIni.Items.Add(string.Format("[{0}]", section.Name));

                    foreach (var key in section.Keys)
                    {
                        this.listBoxSeedBoxIni.Items.Add(key.Name + ":" + key.Value);
                    }
                    this.listBoxSeedBoxIni.Items.Add(Environment.NewLine);
                }

                // Display DownloadedListFileLog.txt
                if (File.Exists(this._ftpSyncWrapper.DownloadedListFileLog))
                {
                    fs = File.OpenRead(this._ftpSyncWrapper.DownloadedListFileLog);

                    richTextBoxNewSeries.LoadFile(fs, RichTextBoxStreamType.PlainText);
                    fs.Close();
                }
                else
                {
                    this.richTextBoxNewSeries.Text = string.Format( Me.Common.Resources.FileNotFound, this._ftpSyncWrapper.DownloadedListFileLog);
                }

            }
            catch (Exception ex)
            {
                Me.Common.Utils.DisplayInMessageBox(ex.Message);
                log.Debug(ex.Message);
                Application.Exit();
            }
            finally
            {
                if(fs != null)
                {
                    fs.Close();
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.Info("");
            if (this.notifyIcon != null)
            {
                this.notifyIcon.Dispose();

            }
            log.InfoFormat(Me.Common.Resources.End, "WinForm",0);

        }

        private void buttonRunFTPSync_Click(object sender, EventArgs e)
        {
            log.Info("");

            this.buttonRunFTPSync.Enabled = false;

            // [10003-003]  ADD: in CFTPSyncWrapper and WFTPSyncWrapper same command line parameters than FTPSync
            // FTPSync parameters the values defined in GUI will replace those defined in app.settings in FTPSyncWrapper.cs.
            this._options.Quiet = this.checkBoxQuiet.Checked;
            this._options.Full = this.radioButtonFull.Checked;
            this._options.Incremental = this.radioButtonIncremental.Checked;
            this._options.Differential = this.radioButtonDifferential.Checked;
            this._options.Init = this.checkBoxInit.Checked;

            // [10003-003]  ADD: in CFTPSyncWrapper and WFTPSyncWrapper same command line parameters than FTPSync
            // New this._ftpSyncWrapper object to pass new options defined in the GUI
            try
            {
                // Using dependency injection
                this._ftpSyncWrapper = new Me.WCFTPSyncWrapper.Factory().GetWCFTPSyncWrapper(_options);
                this.buttonRunFTPSync.Enabled = true;
            }
            catch (Exception ex)
            {
                Me.Common.Utils.DisplayInMessageBox(ex.Message);
                log.Debug(ex.Message);
                Application.Exit();
            }

            try
            {
                this._ftpSyncWrapper.FTPSyncHelperWorkFlow();
                this.buttonRunFTPSync.Enabled = true;

                log.Info(Me.Common.Resources.Success);
            }
            catch (ApplicationException ex)
            {
                Me.Common.Utils.DisplayInMessageBox(ex.Message);
                this.buttonRunFTPSync.Enabled = true;

                log.Info(Me.Common.Resources.Success);
            }
            catch (Exception ex)
            {
                Me.Common.Utils.DisplayInMessageBox(ex.Message);
                this.buttonRunFTPSync.Enabled = true;

                log.Fatal("", ex);
            }
        }

        private class TextBoxWriter : TextWriter
        {
            TextBox output = null;

            public TextBoxWriter(TextBox output)
            {
                this.output = output;
            }

            public override void Write(char value)
            {
                base.Write(value);
                output.AppendText(value.ToString());
            }

            public override Encoding Encoding
            {
                get { return System.Text.Encoding.UTF8; }
            }
        }

        /// <summary>
        /// [10003-003]  ADD: in CFTPSyncWrapper and WFTPSyncWrapper same command line parameters than FTPSync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonIncrementalDifferential_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonDifferential.Checked == true || this.radioButtonIncremental.Checked == true)
            {
                // Init only with /FULL
                this.checkBoxInit.Checked = false;
            }
        }

        /// <summary>
        /// [10003-003]  ADD: in CFTPSyncWrapper and WFTPSyncWrapper same command line parameters than FTPSync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxInit_CheckedChanged(object sender, EventArgs e)
        {
            // /FULL with /INIT
            if (this.checkBoxInit.Checked == true)
            {
                this.radioButtonFull.Checked = true;
            }
        }
    }

}

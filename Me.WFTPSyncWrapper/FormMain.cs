using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MadMilkman.Ini;
using System.IO;
using System.Runtime.InteropServices;

namespace Me.WFTPSyncWrapper
{
    public partial class FormMain : Form
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

            try
            {
                log.Info("");

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
    }

}

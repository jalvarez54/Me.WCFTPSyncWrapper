using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Me.Common;

namespace Me.WCFTPSyncWrapper
{
    /// <summary>
    /// http://www.cyberkiko.com/page/ftpsync/
    /// </summary>
    public class FTPSyncWrapper : IFTPSyncWrapper
    {
        private Options _options; // = new Options();

        /// <summary>
        /// [10003-002]  ADD: object options parmeter in FTPSyncWrapper() constructor
        /// </summary>
        /// <param name="options"></param>
        public FTPSyncWrapper(object options)
        {
            try
            {
                _options = (Options)options;
                Initialize();

            }
            catch (Exception)
            {

                throw;
            }
        }
        private const string FTPSyncFileName = "FTPSync.exe";
        private const string FTPSyncFolderName = @"\FTPSync";


        #region Fields
        private string _seriesPath;
        private string _downloadsPath;
        private string _downloadedListFileLog;
        private bool _quiet = false;
        private bool _full = false;
        private bool _init = false;
        private bool _differential = false;
        private bool _incremental = false;
        private List<string> _seriesName = new List<string>();
        private string[] _downloadedFiles;
        private string[] _seriesFilesPath;
        private Dictionary<string, string> _duplicatedSeriesFiles;
        private string _duplicatedPath;

        #endregion

        #region Properties
        public bool Quiet
        {
            get
            {
                return _quiet;
            }

            set
            {
                _quiet = value;
            }
        }

        public bool Full
        {
            get
            {
                return _full;
            }

            set
            {
                _full = value;
            }
        }

        public bool Init
        {
            get
            {
                return _init;
            }

            set
            {
                _init = value;
            }
        }

        public bool Differential
        {
            get
            {
                return _differential;
            }

            set
            {
                _differential = value;
            }
        }

        public bool Incremental
        {
            get
            {
                return _incremental;
            }

            set
            {
                _incremental = value;
            }
        }

        public string SeriesPath
        {
            get
            {
                return _seriesPath;
            }

            set
            {
                _seriesPath = value;
            }
        }

        public string DownloadsPath
        {
            get
            {
                return _downloadsPath;
            }

            set
            {
                _downloadsPath = value;
            }
        }

        public string DownloadedListFileLog
        {
            get
            {
                return _downloadedListFileLog;
            }

            set
            {
                _downloadedListFileLog = value;
            }
        }

        public string DuplicatedPath
        {
            get
            {
                return _duplicatedPath;
            }

            set
            {
                _duplicatedPath = value;
            }
        }

        public Options Options
        {
            get
            {
                return _options;
            }

            set
            {
                _options = value;
            }
        }

        #endregion

        private void Initialize()
        {

            try
            {
                if (_options != null)
                {
                    this._quiet = _options.Quiet;
                    this._full = _options.Full;
                    this._init = _options.Init;
                    this._differential = _options.Differential;
                    this._incremental = _options.Incremental;
                }
                else
                {
                    this._quiet = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["quiet"]);
                    this._full = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["full"]);
                    this._init = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["init"]);
                    this._differential = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["differential"]);
                    this._incremental = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["incremental"]);
                }
                this._seriesPath = System.Configuration.ConfigurationManager.AppSettings["seriesPath"];
                this._downloadsPath = System.Configuration.ConfigurationManager.AppSettings["downloadsPath"];
                this._downloadedListFileLog = System.Configuration.ConfigurationManager.AppSettings["downloadedListFileLog"];
                this._duplicatedPath = System.Configuration.ConfigurationManager.AppSettings["duplicatedPath"];
            }
            catch (Exception)
            {
                throw;
            }

            // Create Temp folder
            try
            {
                if (!Directory.Exists(_downloadsPath))
                {
                    Directory.CreateDirectory(_downloadsPath);
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                this.GetSeriesName();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void FTPSyncHelperWorkFlow()
        {

            try
            {
                this.LaunchFTPSyncProcess();
                this.GetDownloadFilesName();
                this.MoveDownloadedFiles();
                // TODO: MoveDuplicatedFiles
                //this.MoveDuplicatedFiles();
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// TODO: MoveDuplicatedFiles
        /// </summary>
        private void MoveDuplicatedFiles()
        {
            try
            {

            this.GetSeriesFilesPath();
            this._duplicatedSeriesFiles = new Dictionary<string, string>();
            Dictionary<string, string> nonDuplicatedSeriesFiles = new Dictionary<string, string>();
                if (this._seriesFilesPath.Length != 0)
                {
                    foreach (var filePath in this._seriesFilesPath)
                    {

                        foreach (var serieName in _seriesName)
                        {
                            if (filePath.Contains(serieName))
                            {
                                string fileName = Path.GetFileName(filePath);
                                string key = fileName.Substring(0, serieName.Length + 8) + Path.GetExtension(fileName);

                                if (!nonDuplicatedSeriesFiles.ContainsKey(key))
                                {
                                    nonDuplicatedSeriesFiles.Add(key, filePath);
                                }
                                else
                                {
                                    string fileDestination = this._duplicatedPath + Path.DirectorySeparatorChar + fileName;
                                    try
                                    {
                                        this._duplicatedSeriesFiles.Add(key, filePath);

                                    }
                                    catch (ArgumentException)
                                    {

                                    }
                                    File.Move(filePath, fileDestination);
                                }
                            }
                        }

                    }

                }
                else
                {
                    string message = string.Format(Me.Common.Resources.NotFound, Me.Common.Utils.WhoCalledMe());
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        private void GetSeriesName()
        {

            try
            {
                DirectoryInfo directory = new DirectoryInfo(_seriesPath);
                DirectoryInfo[] directories = directory.GetDirectories();

                foreach (DirectoryInfo folder in directories)
                    _seriesName.Add(folder.Name);

            }
            catch (Exception)
            {
                throw;
            }
        }
        private void GetSeriesFilesPath()
        {

            try
            {
                _seriesFilesPath = Directory.GetFiles(_seriesPath, "*.*", SearchOption.AllDirectories);

            }
            catch (Exception)
            {
                throw;
            }
        }
        private void GetDownloadFilesName()
        {

            try
            {
                _downloadedFiles = Directory.GetFiles(_downloadsPath, "*.*", SearchOption.AllDirectories);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LaunchFTPSyncProcess()
        {

            try
            {
                using (Process ftpsyncProcess = new Process())
                {
                    ftpsyncProcess.StartInfo.RedirectStandardError = true;
                    ftpsyncProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                    ftpsyncProcess.ErrorDataReceived += new DataReceivedEventHandler(OnProcessErrorOutput);
                        if (this._full)
                        {
                            ftpsyncProcess.StartInfo.Arguments = Path.GetFileNameWithoutExtension(this.GetIniFileName()) + " /FULL";
                            // [10001] ADD: /INIT option
                            if (this._init)
                            {
                                ftpsyncProcess.StartInfo.Arguments += " /INIT";
                            }
                        }
                        else
                        {
                            if (this._incremental)
                            {
                                ftpsyncProcess.StartInfo.Arguments = Path.GetFileNameWithoutExtension(this.GetIniFileName()) + " /INCREMENTAL ";
                            }
                            else
                            {
                                if (this._differential)
                                {
                                    ftpsyncProcess.StartInfo.Arguments = Path.GetFileNameWithoutExtension(this.GetIniFileName()) + " /DIFFERENTIAL";
                                }
                            }
                        }

                        if (this._quiet)
                        {
                            ftpsyncProcess.StartInfo.Arguments += " /QUIET";
                        }
                    ftpsyncProcess.StartInfo.FileName = this.GetFTPSyncExePath();
                    ftpsyncProcess.StartInfo.RedirectStandardOutput = true;
                    ftpsyncProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    ftpsyncProcess.StartInfo.UseShellExecute = false;
                    ftpsyncProcess.StartInfo.CreateNoWindow = true;
                    ftpsyncProcess.StartInfo.ErrorDialog = false;
                    ftpsyncProcess.Start();
                    ftpsyncProcess.BeginErrorReadLine();
                    ftpsyncProcess.WaitForExit(); // allows the process to end within 1 minute
                    var exitCode = ftpsyncProcess.ExitCode;
                    ftpsyncProcess.Close();
                    ftpsyncProcess.Dispose();

                    string ftpSyncMessage = string.Empty;
                    switch (exitCode)
                    {
                        case 0:
                            return;
                        case 1:
                            ftpSyncMessage = Me.Common.Resources.FTPSyncError_1;
                            break;
                        case 2:
                            ftpSyncMessage = Me.Common.Resources.FTPSyncError_2;
                            break;

                        case 3:
                            ftpSyncMessage = Me.Common.Resources.FTPSyncError_3;
                            break;

                        case 4:
                            ftpSyncMessage = Me.Common.Resources.FTPSyncError_4;
                            break;
                    }

                    throw new Exception(ftpSyncMessage);

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// FTPSync async redirect errors handler
        /// </summary>
        /// <param name="process"></param>
        /// <param name="errLine"></param>
        private void OnProcessErrorOutput(Object process, DataReceivedEventArgs errLine)
        {

            Process theProcess = (Process)process;
            string message = string.Empty;
            if (theProcess != null)
            {
                switch (theProcess.ExitCode)
                {
                    case 0:
                        message = Me.Common.Resources.FTPSyncError_0;
                        break;
                    case 1:
                        message = Me.Common.Resources.FTPSyncError_1;
                        break;
                    case 2:
                        message = Me.Common.Resources.FTPSyncError_2;
                        break;
                    case 3:
                        message = Me.Common.Resources.FTPSyncError_3;
                        break;
                    case 4:
                        message = Me.Common.Resources.FTPSyncError_4;
                        break;
                    default:
                        message = Me.Common.Resources.FTPSyncError_Undefined;
                        break;
                }

            }
        }

        /// <summary>
        /// Move downloaded files to N:\Videos\Series\<serieName>.
        /// </summary>
        private void MoveDownloadedFiles()
        {

            try
            {
                if (_downloadedFiles.Length != 0)
                {
                    foreach (var serieName in _seriesName)
                    {
                        foreach (var downloadedFile in _downloadedFiles)
                        {
                            if (downloadedFile.Contains(serieName))
                            {

                                // N:\Videos\Series\Better.Call.Saul
                                string serieDir = this._seriesPath + Path.DirectorySeparatorChar + serieName;
                                string fileName = Path.GetFileName(downloadedFile);
                                string fileToSearch = serieDir + Path.DirectorySeparatorChar + fileName;

                                string searchPattern = fileName.Substring(0, serieName.Length + 8) + "*" + Path.GetExtension(fileName);

                                var fileExist = Directory.EnumerateFiles(serieDir, searchPattern, SearchOption.AllDirectories).Any();
                                // if not file exist move downloaded file
                                if (!fileExist)
                                {
                                    // Append message to file (create it if not exist)
                                    string message = string.Format(Me.Common.Resources.NewMovieAdded, DateTime.Now, fileName, serieName) + Environment.NewLine;
                                    File.AppendAllText(this._downloadedListFileLog, message);

                                    File.Move(downloadedFile, fileToSearch);
                                }
                                else
                                {
                                    // clean if not a moved file (file already exist)
                                    File.Delete(downloadedFile);
                                }

                            }
                        }
                    }
                    // clean file structure
                    Directory.Delete(_downloadsPath, true);

                }
                else
                {
                    string messageEx = string.Format(Me.Common.Resources.NoDownload);
                    string message = string.Format("{0}: {1}", DateTime.Now, Me.Common.Resources.NoDownload);
                    File.AppendAllText(this._downloadedListFileLog, message + Environment.NewLine);
                    throw new ApplicationException(messageEx);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private string GetIniFileName()
        {
            string ini = string.Empty;

            try
            {
                ini = System.Configuration.ConfigurationManager.AppSettings["iniFileName"];

            }
            catch (Exception)
            {
                throw;
            }
            return ini;
        }

        private string GetFTPSyncExePath()
        {
            string exePath = string.Empty;

            try
            {
                exePath = this.GetFTPSyncExeDirectory() + Path.DirectorySeparatorChar + FTPSyncWrapper.FTPSyncFileName;

            }
            catch (Exception)
            {
                throw;
            }
            return exePath;
        }

        private string GetFTPSyncExeDirectory()
        {
            string exeFolder = string.Empty;

            try
            {
                //get the full location of the assembly with DaoTests in it
                string fullPath = System.Reflection.Assembly.GetAssembly(typeof(FTPSyncWrapper)).Location;
                //get the folder that's in
                exeFolder = Path.GetDirectoryName(fullPath) + FTPSyncWrapper.FTPSyncFolderName;

            }
            catch (Exception)
            {
                throw;
            }
            return exeFolder;
        }

    }
}

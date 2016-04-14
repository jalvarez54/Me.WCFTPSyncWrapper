using Me.WCFTPSyncWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Me.CFTPSyncWrapper
{
    class Program
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // https://bluehouse.wordpress.com/2006/01/24/how-to-create-a-notify-icon-in-c-without-a-form/
        private static System.Windows.Forms.NotifyIcon _notifyIcon;
        private static Me.WCFTPSyncWrapper.IFTPSyncWrapper _ftpSyncWrapper;
        private static Options options = new Options();
        const string _wFTPSyncWrapper = "WFTPSyncWrapper.exe";

        static void Main(string[] args)
        {
            log.InfoFormat(">>>>>>>>>> BEGIN mode {0}", "Console");

            // Catch all unhandled exceptions
            Application.ThreadException += Application_ThreadException;
            // Catch all unhandled exceptions in all threads.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                // Values are available here
                Console.WriteLine("Version: {0}", options.Version);
            }

            try
            {
                _ftpSyncWrapper = new Me.WCFTPSyncWrapper.Factory().GetWCFTPSyncWrapper();
            }
            catch (Exception ex)
            {
                log.Fatal("", ex);
                Program.EndApp(ex.HResult);
                
            }

            try
            {
                SomeClass sc = new SomeClass();
            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }


            if (args.Length > 0)
            {
                
                if (options.FTPSync)
                {
                    log.Debug("options.FTPSync");
                    try
                    {
                        Program._ftpSyncWrapper.LaunchFTPSyncProcess();
                        log.Info(Me.Common.Resources.Success);
                        Program.EndApp(0);
                    }
                    catch (ApplicationException ex)
                    {
                        Program.DisplayInMessageBox(ex.Message);
                        log.Info(Me.Common.Resources.Success);
                        Program.EndApp(0);
                    }
                    catch (Exception ex)
                    {
                        Program.DisplayInMessageBox(ex.Message);
                        log.Fatal("", ex);
                        Program.EndApp(ex.HResult);
                    }

                }

                if (options.Console)
                {
                    log.Debug("options.Console");
                    // TODO: /FULL /INIT /INCREMENTAL /DIFFERENTIAL 
                    try
                    {
                        ConsoleFTPSyncMainWorkFlow();
                        log.Info(Me.Common.Resources.Success);
                        Program.EndApp(0);
                    }
                    catch (ApplicationException ex)
                    {
                        Program.DisplayInMessageBox(ex.Message);
                        log.Debug(ex.Message);
                        log.Info(Me.Common.Resources.Success);
                        Program.EndApp(0);
                    }
                    catch (Exception ex)
                    {
                        Program.DisplayInMessageBox(ex.Message);
                        log.Debug(ex.Message);
                        Program.EndApp(0);
                    }

                }

                if (options.GUI)
                {
                    log.Debug("options.GUI");
                    try
                    {
                        WinFormMode(args);
                        log.Info(Me.Common.Resources.Success);
                        Program.EndApp(0);

                    }
                    catch (Exception ex)
                    {
                        Program.DisplayInMessageBox(ex.Message);
                        log.Fatal("", ex);
                        Program.EndApp(ex.HResult);
                    }
                }

            }
            else
            {
                // Default (without arguments)
                log.InfoFormat(Me.Common.Resources.Usage, options.GetUsage());
                return;

            }
        }

        private static void WinFormMode(string[] args)
        {
            log.Info("");

            try
            {
                using (Process ftpsyncProcess = new Process())
                {
                    ftpsyncProcess.StartInfo.FileName = Program.GetFTPSyncExePath();
                    ftpsyncProcess.Start();
                    ftpsyncProcess.WaitForExit(); // allows the process to end within 1 minute
                    var exitCode = ftpsyncProcess.ExitCode;
                    log.InfoFormat(Me.Common.Resources.GuiExitCode, exitCode);
                    ftpsyncProcess.Close();
                    ftpsyncProcess.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return;
        }

        private static void ConsoleFTPSyncMainWorkFlow()
        {
            log.Info("");

            try
            {
                Program._ftpSyncWrapper.FTPSyncHelperWorkFlow();
            }
            catch (Exception)
            {
                throw;
            }
            return;
        }
        private static void EndApp(int exitcode = 0)
        {
            log.Info("");

            if (Program._notifyIcon != null)
            {
                Program._notifyIcon.Dispose();

            }
            log.InfoFormat("Exit code: {0}", exitcode);
            log.InfoFormat(">>>>>>>>>> END mode {0}", "Console");
            Environment.Exit(exitcode);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Info("");

            try
            {
                log.Fatal("UnhandledException" + e.ExceptionObject.ToString());
                Program.EndApp(-1);

            }
            catch
            {
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            log.Info("");

            try
            {
                log.Fatal("ThreadException", e.Exception);
                Program.EndApp(-1);
            }
            catch
            {

            }
        }

        private class ControlContainer : IContainer
        {

            ComponentCollection _components;

            public ControlContainer()
            {
                _components = new ComponentCollection(new IComponent[] { });
            }

            public void Add(IComponent component)
            { }

            public void Add(IComponent component, string Name)
            { }

            public void Remove(IComponent component)
            { }

            public ComponentCollection Components
            {
                get { return _components; }
            }

            public void Dispose()
            {
                _components = null;
            }
        }

        private class SomeClass
        {
            ControlContainer container = new ControlContainer();

            public SomeClass()
            {
                Program._notifyIcon = new NotifyIcon(container);
                Program._notifyIcon.Icon = Me.Common.Resources.favicon;
                Program._notifyIcon.Text = Me.Common.Resources.Running;
                // TODO: ShowBalloonTip not showned
                Program._notifyIcon.ShowBalloonTip(10000, Application.ProductName, Program._notifyIcon.Text, ToolTipIcon.Info);
                Program._notifyIcon.Visible = true;
            }
        }

        private static void DisplayInMessageBox(string message)
        {
            if (!_ftpSyncWrapper.Quiet)
            {
                Me.Common.Utils.DisplayInMessageBox(message);
            }
        }

        private static string GetFTPSyncExePath()
        {
            log.Info("");

            string exePath = string.Empty;

            try
            {
                exePath = Program.GetFTPSyncExeDirectory() + Path.DirectorySeparatorChar + Program._wFTPSyncWrapper;
            }
            catch (Exception)
            {

                throw;
            }

            return exePath;
        }

        private static string GetFTPSyncExeDirectory()
        {
            log.Info("");

            string exeFolder = string.Empty;

            try
            {
                //get the full location of the assembly with DaoTests in it
                string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Program)).Location;
                //get the folder that's in
                exeFolder = Path.GetDirectoryName(fullPath);
            }
            catch (Exception)
            {
                throw;
            }
            return exeFolder;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.WCFTPSyncWrapper
{
    public interface IFTPSyncWrapper
    {
        void FTPSyncHelperWorkFlow();
        void LaunchFTPSyncProcess();
        bool Quiet { get; set; }
        bool Full { get; set; }
        bool Differential { get; set; }
        bool Incremental { get; set; }
        string SeriesPath { get; set; }
        string DownloadsPath { get; set; }
        string DownloadedListFileLog { get; set; }
        string DuplicatedPath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.WCFTPSyncWrapper
{
    public class Factory
    {
        public IFTPSyncWrapper GetFTPSyncWrapper(object options)
        {
            return new WCFTPSyncWrapper.FTPSyncWrapper(options);
        }
    }
}

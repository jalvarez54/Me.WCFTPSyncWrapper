﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.WCFTPSyncWrapper
{
    public class Factory
    {
        public IFTPSyncWrapper GetWCFTPSyncWrapper(object options = null)
        {
            return new WCFTPSyncWrapper.FTPSyncWrapper(options);
        }
    }
}

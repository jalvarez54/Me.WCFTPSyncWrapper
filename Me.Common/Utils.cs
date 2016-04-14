using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.Common
{
    public static class Utils
    {
        public static string WhoCalledMe([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            //StackTrace stackTrace = new StackTrace();
            //StackFrame stackFrame = stackTrace.GetFrame(1);
            //MethodBase methodBase = stackFrame.GetMethod();
            //return methodBase.Name;
            // https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callermembernameattribute%28v=vs.110%29.aspx
            return memberName;
        }

        public static void DisplayInMessageBox(string message)
        {
            System.Windows.Forms.MessageBox.Show(message,
                System.Windows.Forms.Application.ProductName + " version: " + System.Windows.Forms.Application.ProductVersion,
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }

    }
}

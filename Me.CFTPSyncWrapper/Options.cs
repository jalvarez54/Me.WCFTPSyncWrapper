using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Me.CFTPSyncWrapper
{
    // Define a class to receive parsed values
    class Options
    {
        [Option('c', "console",
          HelpText = "MeFTPSyncHelper console mode.")]
        public bool Console { get; set; }

        [Option('g', "gui",
          HelpText = "MeFTPSyncHelper GUI mode.")]
        public bool GUI { get; set; }

        [Option('f', "ftp",
            HelpText = "FTPSync directly.")]
        public bool FTPSync { get; set; }

        [Option('v', "version",
            HelpText = "Prints version information to standard output.")]
        public string Version
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string currentVersion = fvi.FileVersion;
                return currentVersion;

            }
        }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

    }
}

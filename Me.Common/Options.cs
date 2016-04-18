using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Me.Common
{
    // Define a class to receive parsed values
    public class Options
    {
        [Option('c', "console",
          HelpText = "MeFTPSyncHelper console mode.")]
        public bool Console { get; set; }

        [Option('q', "quiet",
          HelpText = "/QUIET")]
        public bool Quiet { get; set; }

        [Option('a', "full",
          HelpText = "/FULL")]
        public bool Full { get; set; }

        [Option('i', "init",
          HelpText = "/INIT")]
        public bool Init { get; set; }

        [Option('r', "incremental",
          HelpText = "/INCREMENTAL")]
        public bool Incremental { get; set; }

        [Option('d', "differential",
          HelpText = "/DIFFERENTIAL")]
        public bool Differential { get; set; }

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
                string currentVersion = fvi.ProductVersion;
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

using System;
using System.Collections.Generic;

namespace PRCommander
{
    public class ScriptModel
    {
        public string Arguments { get; set; }
        public string Shell {get;set;} = "pwsh";
        public bool IsShellCommand { get; set; }

        public string ToExecute => IsShellCommand ? $"-c {Arguments}" : Arguments;
        public string StartingDirectory { get; set; }
        public bool ShellExecute {get;set;} = false;
    }
}
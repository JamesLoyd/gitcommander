using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GitCommander
{
    public class ScriptRunner
    {
        //TODO Refactor so that you can generate a process of a script model maybe?
        public string Run(ScriptModel model)
        {
            var p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            if (!model.ShellExecute)
            {
                p.StartInfo.Arguments = model.ToExecute;
            }
            p.StartInfo.FileName = model.Shell;


            if (!string.IsNullOrWhiteSpace(model.StartingDirectory))
            {
                p.StartInfo.WorkingDirectory = model.StartingDirectory;
            }


            if (model.ShellExecute)
            {
                p.StartInfo.RedirectStandardInput = true;
            }

            p.Start();


            if (model.ShellExecute)
            {
                p.StandardInput.WriteLine(@"echo yo");
            }
            else
            {

                // Do not wait for the child process to exit before
                // reading to the end of its redirected stream.
                // p.WaitForExit();
                // Read the output stream first and then wait.

                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                // string error = p.StandardError.ReadToEnd();
                // Console.WriteLine(output);
                p.WaitForExit();

                return output + (string.IsNullOrWhiteSpace(error) ? "" : error);
            }
            return "";
        }
    }
}
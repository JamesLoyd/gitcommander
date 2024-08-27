using Terminal.Gui;
using GitCommander.Models;

namespace GitCommander
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Application.Init();

            try
            {
                Application.Run(new MyView());

            }
            finally
            {
                Application.Shutdown();
            }

        }
    }
}

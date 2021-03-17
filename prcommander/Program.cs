using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Terminal.Gui;
using PRCommander.Models;

namespace PRCommander
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Application.Init();
            var top = Application.Top;
            var window = new Window("GH Commander")
            {
                X = 0,
                Y = 1, //leaves one row for the toplevel menu
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var menu = GetMenu(window);

            var promptLabel = new Label("Search for open PRs?");
            var button = new Button("Confirm")
            {
                X = Pos.Left(promptLabel),
                Y = Pos.Top(promptLabel) + 1,
            };


            var ColorScheme = new ColorScheme()
            {
                Normal = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Blue),
                HotFocus = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Blue),
                Focus = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Blue),
                HotNormal = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Blue)
            };

            var listViewLabel = new Label("Open PR #'s")
            {
                X = Pos.Left(button),
                Y = Pos.Top(button) + 1,
                Width = Dim.Fill(),
                Visible = false
            };

            var listView = new ListView()
            {
                X = Pos.Left(listViewLabel),
                Y = Pos.Top(listViewLabel) + 1,
                Height = Dim.Fill(),
                Width = Dim.Fill(),
                AllowsMarking = false,
                AllowsMultipleSelection = false,
                Visible = false,
            };

            var resultFromOpenPrLabel = new Label("Response from GH CLI")
            {
                X = Pos.Left(listView),
                Y = Pos.Top(listView) + 10,
                Visible = false
            };

            var text = new Label("hello")
            {
                Visible = false,
                X = Pos.Left(resultFromOpenPrLabel),
                Y = Pos.Top(resultFromOpenPrLabel) + 1,
                Width = Dim.Fill(),
                ColorScheme = ColorScheme
            };

            var endButton = new Button("Exit GH Commander")
            {
                X = Pos.Left(text),
                Y = Pos.Top(text) + 1,
                Width = Dim.Fill()
            };



            listView.OpenSelectedItem += (ListViewItemEventArgs arg1) =>
            {
                var argValue = arg1.Value as string;
                if (argValue != null)
                {

                    var testRun = new ScriptModel
                    {
                        Arguments = "gh pr checkout " + argValue.Split(':')[1].TrimStart().TrimEnd(),
                        StartingDirectory = "/home/james/repos/ATGV4",
                        IsShellCommand = true
                    };

                    var output2 = new ScriptRunner().Run(testRun);
                    resultFromOpenPrLabel.Visible = true;
                    text.Visible = true;
                    text.Text = output2;
                }
            };

            button.Clicked += () =>
            {
                listView.Visible = true;
                listViewLabel.Visible = true;

                var script = new ScriptModel
                {
                    Arguments = "test.ps1"
                };

                var output = new ScriptRunner().Run(script);

                var test1 = output.Trim(new char[] { '\uFEFF' }).Split('\n').SkipLast(1).Select((x, y) => new PRResult
                {
                    Index = y,
                    Result = x,
                    PRNumber = new string(x.TakeWhile(z => char.IsDigit(z)).ToArray()),
                    Branch = x.Replace("	", "|").Split('|')[2],
                    Status = x.Replace("	", "|").Split('|')[3]
                });



                var options = test1.Select(ty => $"PR #: {new string(ty.Result.TakeWhile(x => char.IsDigit(x)).ToArray())} ::: Branch {ty.Branch} => Status: {ty.Status}");

                listView.SetSource(options.ToList());
            };



            endButton.Clicked += () =>
            {
                Application.RequestStop();
            };

            window.Add(promptLabel, button, listViewLabel, listView, resultFromOpenPrLabel, text, endButton);

            top.Add(menu);
            top.Add(window);


            Application.Run();
            Application.Shutdown();


            // Start the child process.


        }

        private static MenuBar GetMenu(Window window)
        {
            return new MenuBar(new MenuBarItem[] {new MenuBarItem("_File", new[] {
                new MenuItem("_Quit", "It quits the application", () =>
                {
                    Application.RequestStop();
                })
            })});
        }
    }
}

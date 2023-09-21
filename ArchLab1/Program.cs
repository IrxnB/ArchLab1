using System;
using System.Security.Cryptography.X509Certificates;
using ArchLab1.Holders;
using ArchLab1.Model;
using ArchLab1.View;
using ArchLab1.Controller;
using ArchLab1Lib.Model;

namespace ArchLab1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repo = CsvTaskRepoHolder.GetInstance;
            var view = new ConsoleGuiView();
            var controller = new CsvController(repo, view);

            var isExitCommand = false;
            do
            {
                controller.ShowCommands();
                controller.ProcessCommand(controller.ReadCommand(), out isExitCommand);

            } while (isExitCommand != true);

        }
    }
}
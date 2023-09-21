using ArchLab1Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArchLab1
{
    internal class ConsoleGuiView
    {
        public void DrawCommands(List<Command> commands)
        {
            foreach (Command command in commands) { 
                Console.WriteLine($"{(int)command + 1}. {command}");
            }
        }

        public void DrawTasks(List<TaskEntity> tasks)
        {
            tasks.ForEach(task => DrawTask(task));
        }
        public void DrawTask(TaskEntity? task)
        {
            if(task != null)
                Console.WriteLine($"Task: Id={task.Id}, Name={task.Name}");
        }

        public void AskForCommand()
        {
            Console.WriteLine("Enter command Id:");
        }
        public void AskForId()
        {
            Console.WriteLine("Enter Id:");
        }
    }
}

using ArchLab1Lib;
using ArchLab1Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArchLab1.View
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
                Console.WriteLine($"Task: Id={task.Id}, Name={task.Name}," +
                    $" Desc={task.Description}, Completed={task.IsComplete}");
        }

        public void AskForCommand()
        {
            Console.WriteLine("Enter command Id:");
        }
        public void AskForField(String fieldName)
        {
            Console.WriteLine($"Enter {fieldName}:");
        }
    }
}

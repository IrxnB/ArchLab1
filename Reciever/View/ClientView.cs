﻿using ArchLab1Lib.Model;
using ArchLab1Lib;

namespace Reciever.View
{
    public class ClientView
    {
        public void DrawCommands(List<Command> commands)
        {
            foreach (Command command in commands)
            {
                Console.WriteLine($"{(int)command + 1}. {command}");
            }
        }

        public void DrawStrings(List<string> tasks)
        {
            tasks.ForEach(task => Console.WriteLine(task));
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
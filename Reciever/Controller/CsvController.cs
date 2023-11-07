using ArchLab1Lib.Model;
using ArchLab1Lib;
using Reciever.View;

namespace Client.Controller
{
    internal class CsvController
    {
        private readonly ClientView _view;
        private readonly List<Command> _commands;

        public CsvController(ClientView view)
        {
            _view = view;
            _commands = Enum.GetValues(typeof(Command))
                .AsQueryable()
                .Cast<Command>()
                .ToList();
        }
        public Request ProcessCommand(Command? command, out bool isExitCommand, out bool validInput)
        {
            validInput = true;
            isExitCommand = false;
            var req = new Request();
            req.Command = command;
            switch (command)
            {
                case Command.GetById:
                    {
                        long id = ReadIdFromConsole(out validInput);
                        if(validInput){
                            var body = new TaskEntity();
                            body.TaskEntityId = id;
                            req.Body = body;
                        }
                        break;
                    }
                case Command.CreateNew:
                    {
                        TaskEntity fromConsole = ReadTaskFromConsole(out validInput);
                        if (validInput)
                        {
                            req.Body = fromConsole;
                        }
                        break;
                    }
                case Command.UpdateById:
                    {
                        TaskEntity fromConsole = ReadTaskWithIdFromConsole(out validInput);
                        if (validInput)
                        {
                            req.Body = fromConsole;
                        }
                        break;
                    }
                case Command.DeleteById:
                    {
                        long id = ReadIdFromConsole(out validInput);
                        if (validInput)
                        {
                            var body = new TaskEntity();
                            body.TaskEntityId = id;
                            req.Body = body;
                        }
                        break;
                    }
                case Command.Exit:
                    {
                        isExitCommand = true;
                        break;
                    }
            }
            return req;
        }
        public void ShowCommands()
        {
            _view.DrawCommands(_commands);
        }

        public Command? ReadCommand()
        {
            var commandStr = Console.ReadLine() ?? "";
            Object command = null; 
            if (!Enum.TryParse(typeof(Command), commandStr, out command))
            {
                Console.WriteLine("Wrong Command");
                return null;
            }
            return (Command)command;
        }

        private ArchLab1Lib.Model.TaskEntity ReadTaskWithIdFromConsole(out bool succeed)
        {
            bool idSucceed, readSucceed;
            long id = ReadIdFromConsole(out idSucceed);
            var task = ReadTaskFromConsole(out readSucceed);

            succeed = idSucceed && readSucceed;
            task.TaskEntityId = id;
            return task;
        }
        private ArchLab1Lib.Model.TaskEntity ReadTaskFromConsole(out bool succeed)
        {
            bool nameSucceed = false, descSucceed = false, isCompleteSucceed = false;

            _view.AskForField("name");
            string? name = ReadStringFromConsole(out nameSucceed);

            _view.AskForField("description");
            string? description = ReadStringFromConsole(out descSucceed);

            _view.AskForField("is completed");
            bool isComplete = ReadBoolFromConsole(out isCompleteSucceed);

            succeed = nameSucceed && descSucceed && isCompleteSucceed;
            return new TaskEntity { TaskEntityId = null, Name = name, Description = description, IsComplete = isComplete };
        }
        private long ReadIdFromConsole(out bool succeed)
        {
            _view.AskForField("TaskEntityId");
            long id;
            succeed = long.TryParse(Console.ReadLine(), out id);
            return id;
        }

        private bool ReadBoolFromConsole(out bool succeed)
        {
            bool value;
            succeed = bool.TryParse(Console.ReadLine(), out value);
            return value;
        }
        private string? ReadStringFromConsole(out bool succeed)
        {
            succeed = true;
            string? value = Console.ReadLine();
            if (value == null) succeed = false;
            return value;
        }


    }
}

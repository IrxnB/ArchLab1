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
        public Request ProcessCommand(Command command, out bool isExitCommand)
        {
            isExitCommand = false;
            var req = new Request();
            req.Command = command;
            switch (command)
            {
                case Command.GetById:
                    {
                        bool succeed;
                        long id = ReadIdFromConsole(out succeed);
                        if(succeed){
                            req.Body = new TaskEntity(id);
                        }
                        break;
                    }
                case Command.CreateNew:
                    {
                        bool isCreated = true;
                        TaskEntity fromConsole = ReadTaskFromConsole(out isCreated);
                        if (isCreated)
                        {
                            req.Body = fromConsole;
                        }
                        break;
                    }
                case Command.UpdateById:
                    {
                        bool isCreated = true;
                        TaskEntity fromConsole = ReadTaskFromConsole(out isCreated);
                        if (isCreated)
                        {
                            req.Body = fromConsole;
                        }
                        break;
                    }
                case Command.DeleteById:
                    {
                        bool idSucceed;
                        long id = ReadIdFromConsole(out idSucceed);
                        if (idSucceed)
                        {
                            req.Body = new TaskEntity(id);
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

        public Command ReadCommand()
        {
            var commandStr = Console.ReadLine() ?? "";
            var commandInt = int.Parse(commandStr) - 1;
            var command = (Command)commandInt;
            return command;
        }

        private TaskEntity ReadTaskFromConsole(out bool succeed)
        {
            bool idSucceed = false, nameSucceed = false, descSucceed = false, isCompleteSucceed = false;
            long id = ReadIdFromConsole(out idSucceed);

            _view.AskForField("name");
            string? name = ReadStringFromConsole(out nameSucceed);

            _view.AskForField("description");
            string? description = ReadStringFromConsole(out descSucceed);

            _view.AskForField("is completed");
            bool isComplete = ReadBoolFromConsole(out isCompleteSucceed);

            succeed = idSucceed && nameSucceed && descSucceed && isCompleteSucceed;
            return new TaskEntity(id, name, description, isComplete);
        }
        private long ReadIdFromConsole(out bool succeed)
        {
            _view.AskForField("Id");
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

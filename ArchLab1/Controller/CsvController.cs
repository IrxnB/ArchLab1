using ArchLab1.Holders;
using ArchLab1.Model;
using ArchLab1Lib.Model;
using ArchLab1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1.Controller
{
    internal class CsvController
    {
        private readonly CsvRepository<TaskEntity, long> _repo;
        private readonly ConsoleGuiView _view;
        private readonly List<Command> _commands;

        public CsvController(CsvRepository<TaskEntity, long> repo, ConsoleGuiView view)
        {
            _repo = repo;
            _view = view;
            _commands = Enum.GetValues(typeof(Command))
                .AsQueryable()
                .Cast<Command>()
                .ToList();
        }
        public void ProcessCommand(Command command, out bool isExitCommand)
        {
            isExitCommand = false;
            switch (command)
            {
                case Command.GetAll:
                    {
                        _view.DrawTasks(_repo.ReadAll());
                        break;
                    }
                case Command.GetById:
                    {
                        bool succeed;
                        long id = ReadIdFromConsole(out succeed);
                        if(succeed){
                            _view.DrawTask(_repo.ReadById(id));
                        }
                        break;
                    }
                case Command.CreateNew:
                    {
                        bool isCreated = true;
                        TaskEntity fromConsole = ReadTaskFromConsole(out isCreated);
                        if (isCreated && !_repo.ExistsById(fromConsole.Id))
                        {
                            _repo.Create(fromConsole);
                        }
                        break;
                    }
                case Command.UpdateById:
                    {
                        bool isCreated = true;
                        TaskEntity fromConsole = ReadTaskFromConsole(out isCreated);
                        if (isCreated && _repo.ExistsById(fromConsole.Id))
                        {
                            _repo.Update(fromConsole);
                        }
                        break;
                    }
                case Command.DeleteById:
                    {
                        bool idSucceed;
                        long id = ReadIdFromConsole(out idSucceed);
                        if (idSucceed)
                        {
                            _repo.DeleteById(id);
                        }
                        break;
                    }
                case Command.Exit:
                    {
                        isExitCommand = true;
                        break;
                    }
            }
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

            _view.AskForField("is created");
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

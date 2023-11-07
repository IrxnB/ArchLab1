using ArchLab1.Model;
using ArchLab1Lib.Model;
using ArchLab1Lib;
using Server.View;
using Server.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ArchLab1.Controller
{
    internal class CsvWebController
    {
        private readonly ArchDbContext _db;
        private readonly WebView _view;
        private static Response NotOkResponse = new Response { Status = Status.NOT_OK };

        public CsvWebController(DbContextOptions dbContextOptions, WebView view, ArchDbContext dbContext)
        {
            _db = dbContext;
            _view = view;
        }

        public Response ProcessCommand(Request request)
        {
            switch (request.Command)
            {
                case Command.GetAll:
                    {
                        return _view.Tasks(_db.Tasks.Select(x => x).ToList());
                    }
                case Command.GetById:
                    {
                        if (request.Body.TaskEntityId == null)
                        {
                            return NotOkResponse;
                        }
                        var fromRepo = _db.Tasks.Find(request.Body.TaskEntityId);
                        if (fromRepo == null)
                        {
                            return NotOkResponse;
                        }
                        return _view.Task(fromRepo);
                    }
                case Command.CreateNew:
                    {
                        if (request.Body.TaskEntityId == null)
                        {
                            _db.Tasks.Add(request.Body);
                            _db.SaveChanges();
                            return new Response();
                        }
                        return NotOkResponse;
                    }
                case Command.UpdateById:
                    {
                        var fromDb = _db.Tasks.Find(request.Body.TaskEntityId);
                        if (fromDb != null)
                        {
                            _db.Tasks.Update(request.Body);
                            _db.SaveChanges();
                            return new Response();
                        }
                        return NotOkResponse;
                    }
                case Command.DeleteById:
                    {

                        if (request.Body.TaskEntityId != null)
                        {
                            _db.Tasks.Where(task => task.TaskEntityId == request.Body.TaskEntityId).ExecuteDelete();
                            _db.SaveChanges();
                            return new Response();
                        }
                        return NotOkResponse;
                    }

            }
            return NotOkResponse;

        }


    }
}

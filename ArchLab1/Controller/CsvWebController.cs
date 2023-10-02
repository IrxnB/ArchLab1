using ArchLab1.Holders;
using ArchLab1.Model;
using ArchLab1Lib.Model;
using ArchLab1Lib;
using Server.View;

namespace ArchLab1.Controller
{
    internal class CsvWebController
    {
        private readonly CsvRepository<TaskEntity, long> _repo;
        private readonly WebView _view;
        private static Response NotOkResponse = new Response { Status = Status.NOT_OK };

        public CsvWebController(CsvRepository<TaskEntity, long> repo, WebView view)
        {
            _repo = repo;
            _view = view;
        }
        public Response ProcessCommand(Request request)
        {

            switch (request.Command)
            {
                case Command.GetAll:
                    {
                        return _view.Tasks(_repo.ReadAll());
                    }
                case Command.GetById:
                    {
                        if (request.Body.Id == -1)
                        {
                            return NotOkResponse;
                        }
                        var fromRepo = _repo.ReadById(request.Body.Id);
                        if (fromRepo == null)
                        {
                            return NotOkResponse;
                        }
                        return _view.Task(fromRepo);
                    }
                case Command.CreateNew:
                    {
                        if (request.Body.Id != -1 && !_repo.ExistsById(request.Body.Id))
                        {
                            _repo.Create(request.Body);
                            return new Response();
                        }
                        return NotOkResponse;
                    }
                case Command.UpdateById:
                    {
                        if (_repo.ExistsById(request.Body.Id))
                        {
                            _repo.Update(request.Body);
                            return new Response();
                        }
                        return NotOkResponse;
                    }
                case Command.DeleteById:
                    {
                        
                        if (request.Body.Id != -1)
                        {
                            _repo.DeleteById(request.Body.Id);
                            return new Response();
                        }
                        return NotOkResponse;
                    }
                    
            }
            return NotOkResponse;
        }


    }
}

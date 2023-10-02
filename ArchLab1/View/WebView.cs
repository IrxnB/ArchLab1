using ArchLab1Lib.Model;
using ArchLab1Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    internal class WebView
    {
        public Response Tasks(List<TaskEntity> tasks)
        {
            Response response = new Response();
            tasks.ForEach(task =>
            {response.Add(getTaskString(task));
            });
            return response;
        }
        public Response Task(TaskEntity task)
        {
            Response response = new Response();
            response.Add(getTaskString(task));
            return response;
        }
        private String getTaskString(TaskEntity task)
        {
            return ($"Task: Id={task.Id}, Name={task.Name}," +
                $" Desc={task.Description}, Completed={task.IsComplete}");
        }
    }
}

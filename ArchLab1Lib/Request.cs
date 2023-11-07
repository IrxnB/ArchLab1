using ArchLab1Lib.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1Lib
{
    [Serializable]
    public class Request
    {
        public Command? Command { get; set; }
        public TaskEntity Body { get; set; } = TaskEntity.Empty;

    }
}

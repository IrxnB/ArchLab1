using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1Lib
{
    [Serializable]
    public class Response
    {
        public Status Status { get; set; } = Status.OK;
        public List<String> strings = new List<string>();
        public void Add(string str) => strings.Add(str);
    }
}

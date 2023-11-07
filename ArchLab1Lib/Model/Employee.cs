using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


namespace ArchLab1Lib.Model
{
    [Serializable]
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsWorking { get; set; }

        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}

using System.Data;

namespace ArchLab1Lib.Model
{
    [Serializable]
    public class TaskEntity
    {
        public long? TaskEntityId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; }
        public static TaskEntity Empty = new TaskEntity { TaskEntityId = null, Name = "", Description = "" };
    }
}
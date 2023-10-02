using System.Data;

namespace ArchLab1Lib.Model
{
    [Serializable]
    public class TaskEntity : IEntity<long, TaskEntity>
    {
        public static TaskEntity Empty = new TaskEntity(-1, "", "", false);
        public long Id { get; set; }

        private string? name;
        private string? description;
        public bool IsComplete { get; set; }

        public TaskEntity(long Id, string Name = "", string Description = "", bool IsComplete = false)
        {
            this.Id = Id;
            this.name = Name;
            this.description = Description;
            this.IsComplete = IsComplete;
        }

        public string Name
        {
            get => name ?? "undefined";
            set => this.name = value;
        }
        public string Description
        { 
            get => description ?? "undefined";
            set => this.description = value;
        }

        private void update(TaskEntity task)
        {
            this.name = task.Name;
            this.description = task.Description;
            this.IsComplete = task.IsComplete;
        }

        public void Update(TaskEntity newEntity)
        {
            update(newEntity);
        }
    }
}
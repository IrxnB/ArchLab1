using System.Data;

namespace ArchLab1Lib.Model
{
    //Список задач в проекте
    public class TaskEntity : IEntity<long>
    {
        public long Id { get; set; }
        private string? name;

        public TaskEntity(long Id, string Name)
        {
            this.Id = Id;
            this.name = Name;
        }

        public string Name
        {
            get
            {
                return name ?? "undefined";
            }
            set
            {
                this.name = value;
            }
        }

        private void update(TaskEntity task)
        {
            this.name = task.Name;
        }

        public void Update(IEntity<long> newEntity)
        {
            if (!(newEntity is TaskEntity)) throw new ArgumentException();
            update((TaskEntity)newEntity);
        }
    }
}
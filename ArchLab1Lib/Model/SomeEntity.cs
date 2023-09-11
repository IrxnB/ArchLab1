namespace ArchLab1Lib.Model
{
    public class SomeEntity
    {
        public long Id { get; set; }
        private string? name;
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
    }
}
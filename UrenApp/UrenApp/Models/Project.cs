using SQLite;

namespace UrenApp.Models
{   
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectLeader { get; set; }
        public string Customer { get; set; }
    }
}

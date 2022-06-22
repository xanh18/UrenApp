using SQLite;

namespace UrenApp.Models
{   
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectLeader { get; set; }
        public string Customer { get; set; }
        public int Total { get; set; }
    }
}

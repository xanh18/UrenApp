
namespace UrenApp.Models
{
    public class Hours
    {
        public int Id { get; set; }

        public int? ProjectId { get; set; }

        public string Name { get; set; }

        public int? FrontEnd { get; set; }

        public int? BackEnd { get; set; }

        public bool? IsActive { get; set; }

        public Project Project { get; set; }
    }
}

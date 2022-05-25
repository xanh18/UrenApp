using System.Collections.Generic;
using System.Threading.Tasks;
using UrenApp.Models;

namespace Uregenregistratie.Services
{
    public interface IProjectService
    {
        Task AddProject(string name, string description, string projectleader, string customer);
        Task<IEnumerable<Project>> GetProject();
        Task<Project> GetProject(int id);
        Task RemoveProject(int id);
    }
}

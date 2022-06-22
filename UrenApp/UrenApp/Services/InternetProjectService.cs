using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Models;

namespace UrenApp.Services
{
    public static class InternetProjectService
    {
        static string baseUrl = "http://10.0.2.2:7143";

        static HttpClient client;

        static InternetProjectService()
        {   
           
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            
        }

        public static async Task AddProject(string name, string description, string projectleader, string customer)
        {
            var project = new Project
            {
                Name = name,
                Description = description,
                ProjectLeader = projectleader,
                Customer = customer,          
            };

            var response = await client.PostAsJsonAsync("/api/projects", project);

            if (!response.IsSuccessStatusCode)
            {
                        
            }
        }

        public static async Task RemoveProject(int id)
        {

            var response = await client.DeleteAsync($"/api/projects/{id}");

            if (!response.IsSuccessStatusCode)
            {

            }
        }
        public static async Task<IEnumerable<ProjectResponse>> GetProject()
        {
            var projects = await client.GetFromJsonAsync<List<ProjectResponse>>("/api/projects");

            return projects;
        }

        public static async Task<Project> GetProject(int id)
        {
            var json = await client.GetStringAsync("/api/projects{id}");
            var project = JsonConvert.DeserializeObject<IEnumerable<Project>>(json);
            return (Project)project;
        }


    }
}

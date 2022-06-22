using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Models;
using System.Net.Http.Json;

namespace UrenApp.Services
{
    public static class HoursService
    {
        static string baseUrl = "http://10.0.2.2:7143";

        static HttpClient client;

        static HoursService()
        {

            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

        }

        static Random random = new Random();


        public static async Task AddHours(string ProjectId, string Name, string FrontEnd, string BackEnd, bool IsActive = false,Project test = null)
        {

            var pojectdata = await client.GetAsync($"/api/projects/{ProjectId}");

            if (!pojectdata.IsSuccessStatusCode)
            {
                throw new ArgumentException("Project id bestaat niet");

            }

        /*    if (int.Parse(FrontEnd)+ int.Parse(FrontEnd) != 8)
            {
                throw new ArgumentException("zijn geen 8 gewerkte uren");

            }*/

            var hours = new Hours
            {
                ProjectId = int.Parse(ProjectId),
                Name = Name,
                FrontEnd = int.Parse(FrontEnd),
                BackEnd = int.Parse(FrontEnd),
                IsActive = IsActive,
                Project = test,
            };

            var response = await client.PostAsJsonAsync("/api/hours", hours);

            if (!response.IsSuccessStatusCode)
            {
     
            }
        }

        public static async Task RemoveHours(int id)
        {

            var response = await client.DeleteAsync($"/api/hours/{id}");

            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task VerifyHours( int id ,Hours hours)
        {
            var content = new Hours
            {
                Id = hours.Id,
                ProjectId = hours.ProjectId,
                Name = hours.Name,
                FrontEnd = hours.FrontEnd,
                BackEnd = hours.BackEnd,
                IsActive = true,
                Project = null,
            };


            var response = await client.PutAsJsonAsync($"/api/hours/verify/{id}", content);

            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task<IEnumerable<Hours>> GetHours()
        {
            var json = await client.GetStringAsync("/api/hours");
            var hours = JsonConvert.DeserializeObject<IEnumerable<Hours>>(json);
            return hours;
        }

        /*      public static async Task<Project> GetProject(int id)
              {
                  await Init();

                  var projects = await db.Table<Project>()
                      .FirstOrDefaultAsync(p => p.Id == id);

                  return projects;
              }*/


    }
}

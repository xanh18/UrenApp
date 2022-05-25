using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Uregenregistratie.Services
{
    public  static class ProjectService
    {
        static SQLiteAsyncConnection db;
        async static Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Project>();
        }

        public static async Task AddProject(string name, string description, string projectleader, string customer)
        {
            await Init();
            var project = new Project
            {
                Name = name,
                Description = description,
                ProjectLeader =  projectleader,
                Customer = customer
            };

            var id = await db.InsertAsync(project);
        }

        public static async Task RemoveProject(int id)
        {

            await Init();

            await db.DeleteAsync<Project>(id);
        }

        public static async Task<IEnumerable<Project>> GetProject()
        {
            await Init();

            var project = await db.Table<Project>().ToListAsync();
            return project;
        }

        public static async Task<Project> GetProject(int id)
        {
            await Init();

            var projects = await db.Table<Project>()
                .FirstOrDefaultAsync(p => p.Id == id);

            return projects;
        }

    }
}

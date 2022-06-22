using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uregenregistratie.Services;
using UrenApp.Models;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task AddProject()
        {
            //arange

            string name = "my test project";
            string description = "it was good";
            string projectleader = "me";
            string customer = "a paying one";
            String dbFileName = Guid.NewGuid().ToString() + ".db";
            var db = new SQLiteAsyncConnection(dbFileName);

            try
            {
                await ProjectService.Init(dbFileName);

                //act
                await ProjectService.AddProject(name, description, projectleader, customer);

                //assert
                var projects = await db.QueryAsync<Project>("SELECT * FROM PROJECT");

                projects.Should().ContainSingle();

                var firstProject = projects.First();
                firstProject.Name.Should().Be(name);
                firstProject.Description.Should().Be(description);
                firstProject.ProjectLeader.Should().Be(projectleader);
                firstProject.Customer.Should().Be(customer);

            }
            finally
            {
                await db.CloseAsync();

                File.Delete(dbFileName);
                ProjectService.Reset();
            }
     

        }

        [TestMethod]
        public async Task GetProject()
        {

            //arange

            String dbFileName = Guid.NewGuid().ToString() + ".db";
            var db = new SQLiteAsyncConnection(dbFileName);

            await ProjectService.Init(dbFileName);

            var project = new Project
            {
                Name = "my first project",
                Description = "dsfsg",
                ProjectLeader = "sdgsg",
                Customer = "gdrsgdg"
            };

            await db.InsertAsync(project);

            try
            {
                //act
                var projects = await ProjectService.GetProject();

                //assert
                projects.Should().ContainSingle();

                var firstProject = projects.First();
                firstProject.Name.Should().Be("my first project");

            }
            finally
            {
                await db.CloseAsync();

                File.Delete(dbFileName);
                ProjectService.Reset();
            }

        }



        [TestMethod]
        public async Task RemoveProject()
        {

            //arange

            String dbFileName = Guid.NewGuid().ToString() + ".db";
            var db = new SQLiteAsyncConnection(dbFileName);

            await ProjectService.Init(dbFileName);

            var project = new Project
            {
                Name = "another project",
                Description = "dsfsg",
                ProjectLeader = "xanh",
                Customer = "gdrsgdg"
            };

            var projectId = await db.InsertAsync(project);

            try
            {
                //act
                await ProjectService.RemoveProject(projectId);

                //assert
                var projects = await db.QueryAsync<Project>("SELECT * FROM PROJECT");

                projects.Should().BeEmpty();

            }
            finally
            {
                await db.CloseAsync();

                File.Delete(dbFileName);
                ProjectService.Reset();
            }


        }

        [TestMethod]
        public async Task GetProject_SingleItem()
        {

            //arange

            String dbFileName = Guid.NewGuid().ToString() + ".db";
            var db = new SQLiteAsyncConnection(dbFileName);

            await ProjectService.Init(dbFileName);

            var projectInserted = new Project
            {
                Name = "another project",
                Description = "dsfsg",
                ProjectLeader = "xanh",
                Customer = "gdrsgdg"
            };

            var projectInserted2 = new Project
            {
                Name = "and another project",
                Description = "dsfsg",
                ProjectLeader = "xanh",
                Customer = "gdrsgdg"
            };


            var projectId = await db.InsertAsync(projectInserted);
            var projectId2 = await db.InsertAsync(projectInserted2);

            try
            {
                //act
                var project = await ProjectService.GetProject(projectId);

                //assert
                project.Should().BeEquivalentTo(projectInserted);
                project.Should().NotBeEquivalentTo(projectInserted2);


            }
            finally
            {
                await db.CloseAsync();

                File.Delete(dbFileName);
                ProjectService.Reset();
            }


        }


    }
}

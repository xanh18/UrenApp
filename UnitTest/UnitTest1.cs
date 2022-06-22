using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Uregenregistratie.Services;
using UrenApp.Models;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Blub()
        {
            Assert.IsTrue(true);
        }


        [Fact]
        public async Task AddProject()
        {

            //arange

            string name = "my test project";
            string description = "it was good";
            string projectleader = "me";
            string customer = "a paying one";

            //act
            await ProjectService.AddProject(name, description, projectleader, customer);

            //assert
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            var db = new SQLiteAsyncConnection(databasePath);
            var projects = await db.QueryAsync<Project>("SELECT * FROM PROJECT");

            //projects.Should().ContainSingle();

            /*  //arragen

              var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

              var db = new SQLiteAsyncConnection(databasePath);

              var test =  db.CreateTableAsync<Project>();

              test.UseInMemoryDatabase("MyDb-1");

              var context = new db(contextOptionsBuilder.Options);

              context.LostItems.Add(new LostItem { Id = 1, Description = "verloren schoen  zaal 2", Item = "context" });
              context.SaveChanges();

              var controller = new LostItemsController(context);

              //act
              var result = await controller.Index();

              //assert
              result.Should().BeAssignableTo<ViewResult>();

              var viewResult = (ViewResult)result;
              viewResult.Model.Should().BeAssignableTo<IEnumerable<LostItem>>();

              var items = (IEnumerable<LostItem>)viewResult.Model;

              items.Should().HaveCount(1);*/

            //return Task.CompletedTask;

        }
    }
}

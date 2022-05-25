using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uregenregistratie.Services;
using UrenApp.Models;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;


namespace UrenApp.ViewModels
{

    public class ProjectsViewModel : ViewModelBase
    {

        public ObservableRangeCollection<Project> Project { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Project> RemoveCommand { get; }

        public AsyncCommand<object> SelectedCommand { get; }


        public ProjectsViewModel()
        {
            Title = "Project";

            Project = new ObservableRangeCollection<Project>();

            RemoveCommand = new AsyncCommand<Project>(Remove);
            AddCommand = new AsyncCommand(Add);
            RefreshCommand = new AsyncCommand(Refresh);
         /*   SelectedCommand = new AsyncCommand<object>(Selected);*/
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name");
            var description = await App.Current.MainPage.DisplayPromptAsync("Description", "Description");
            var projectLeader = await App.Current.MainPage.DisplayPromptAsync("ProjectLeader", "ProjectLeader");
            var customer = await App.Current.MainPage.DisplayPromptAsync("Customer", "Customer");

            await ProjectService.AddProject(name, description, projectLeader, customer);

            await Refresh();


        }

        async Task Remove(Project project)
        {
            await ProjectService.RemoveProject(project.Id);
            await Refresh();
        }


        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Project.Clear();

            var project = await ProjectService.GetProject();

            Project.AddRange(project);

            IsBusy = false;
        }

    };

}

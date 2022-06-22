using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Models;
using UrenApp.Services;
using Xamarin.Essentials;

namespace UrenApp.ViewModels
{
    public class InternetProjectViewModel : ViewModelBase
    {
        public ObservableRangeCollection<ProjectResponse> Project { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }

        public AsyncCommand MailCommand { get; }
        public AsyncCommand<Project> RemoveCommand { get; }

        public AsyncCommand<object> SelectedCommand { get; }


        public InternetProjectViewModel()
        {
            Title = "Project Internet";

            Project = new ObservableRangeCollection<ProjectResponse>();

            RemoveCommand = new AsyncCommand<Project>(Remove);
            AddCommand = new AsyncCommand(Add);
            RefreshCommand = new AsyncCommand(Refresh);
            MailCommand = new AsyncCommand(SendEmail);
      /*      SelectedCommand = new AsyncCommand<object>(Selected);*/
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name");
            var description = await App.Current.MainPage.DisplayPromptAsync("Description", "Description");
            var projectLeader = await App.Current.MainPage.DisplayPromptAsync("ProjectLeader", "ProjectLeader");
            var customer = await App.Current.MainPage.DisplayPromptAsync("Customer", "Customer");

            await InternetProjectService.AddProject(name, description, projectLeader, customer);

            await Refresh();


        }

       
        public async Task SendEmail()
        {
            var subject = await App.Current.MainPage.DisplayPromptAsync("Subject", "Subject");
            var body = await App.Current.MainPage.DisplayPromptAsync("Body", "Body");
            var recipients = await App.Current.MainPage.DisplayPromptAsync("Recipients", "Recipients");

            var recpientsList = new List<string>();

            recpientsList.Add(recipients);

            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recpientsList,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }

           // await InternetProjectService.RemoveProject(Project.Id);

            await Refresh();
        }
        



        async Task Remove(Project project)
        {
            await InternetProjectService.RemoveProject(project.Id);
            await Refresh();
        }


        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Project.Clear();

            var project = await InternetProjectService.GetProject();

            Project.AddRange(project);

            IsBusy = false;
        }

    }
}

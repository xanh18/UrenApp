using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Models;
using UrenApp.Services;

namespace UrenApp.ViewModels
{
    public class HoursViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Hours> Hours { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Hours> RemoveCommand { get; }

        public AsyncCommand<Hours> VerifyCommand { get; }

        //public AsyncCommand<object> SelectedCommand { get; }


        public HoursViewModel()
        {
            Title = "Hours hours";

            Hours = new ObservableRangeCollection<Hours>();

            RemoveCommand = new AsyncCommand<Hours>(Remove);
            AddCommand = new AsyncCommand(Add);
            RefreshCommand = new AsyncCommand(Refresh);
            VerifyCommand = new AsyncCommand<Hours>(Verify);
            //SelectedCommand = new AsyncCommand<object>(Selected);
        }

        async Task Add()
        {
            var ProjectId = await App.Current.MainPage.DisplayPromptAsync("ProjectId", "ProjectId");
            var Name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name");
            var FrontEnd = await App.Current.MainPage.DisplayPromptAsync("FrontEnd", "FrontEnd");
            var BackEnd = await App.Current.MainPage.DisplayPromptAsync("BackEnd", "BackEnd");

            await HoursService.AddHours(ProjectId, Name, FrontEnd, BackEnd);

            await Refresh();

        }

        async Task Remove(Hours Hours)
        {
            await HoursService.RemoveHours(Hours.Id);
            await Refresh();
        }

        async Task Verify(Hours Hours)
        {
            await HoursService.VerifyHours(Hours.Id, Hours);
            await Refresh();
        }



        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Hours.Clear();

            var hours = await HoursService.GetHours();

            Hours.AddRange(hours);

            IsBusy = false;
        }

    }
}

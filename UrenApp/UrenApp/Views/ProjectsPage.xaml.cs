using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Models;
using UrenApp.ViewModels;
using UrenApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UrenApp.Views
{
    public partial class ItemsPage : ContentPage
    {
        ProjectsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ProjectsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
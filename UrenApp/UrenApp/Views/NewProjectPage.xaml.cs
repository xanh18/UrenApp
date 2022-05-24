using System;
using System.Collections.Generic;
using System.ComponentModel;
using UrenApp.Models;
using UrenApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UrenApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Projects Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewProjectViewModel();
        }
    }
}
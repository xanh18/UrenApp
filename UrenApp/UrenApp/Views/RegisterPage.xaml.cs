﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UrenApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var username = UserName.Text;
            var password = PassWord.Text;


            await UsersService.Register(username, password);
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }


    }
}
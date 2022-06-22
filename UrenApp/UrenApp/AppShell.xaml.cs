using System;
using System.Collections.Generic;
using UrenApp.Views;
using Xamarin.Forms;

namespace UrenApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(SomeContentPage),
                typeof(SomeContentPage));

            Routing.RegisterRoute(nameof(InternetProjectPage),
                typeof(InternetProjectPage));

            Routing.RegisterRoute(nameof(HoursPage),
                typeof(HoursPage));

            Routing.RegisterRoute(nameof(CameraPage),
                typeof(CameraPage));

        }

     
    }
}

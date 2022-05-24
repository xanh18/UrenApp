using System.ComponentModel;
using UrenApp.ViewModels;
using Xamarin.Forms;

namespace UrenApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProjectDetailViewModel();
        }
    }
}
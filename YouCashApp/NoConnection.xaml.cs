using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoConnection : ContentPage
    {
        public NoConnection()
        {
            InitializeComponent();
            Display();
        }

        private async void Display()
        {
            await DisplayAlert("Connection Error", "Please Connect to Internet and Try Again", "OK");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
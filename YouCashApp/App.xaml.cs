using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YouCashApp
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental", "Brush_Experimental" });
            InitializeComponent();
        }

       

        protected override void OnStart()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                MainPage = new NavigationPage(new Campaign());
            }
            else
            {
                MainPage = new NavigationPage(new NoConnection());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

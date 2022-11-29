using System;
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
            MainPage = new NavigationPage(new LandPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

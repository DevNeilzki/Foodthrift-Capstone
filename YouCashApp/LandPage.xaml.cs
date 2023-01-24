using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandPage : ContentPage
    {
        public LandPage()
        {
            InitializeComponent();

           /* var LoginPage = new NavigationPage(new LandPage());
            var DonorHomepage = new NavigationPage(new DonorHomepage());
            var Homepage = new NavigationPage(new Homepage());

            var loginStatus = Preferences.Get("loginStatus", "0");
            var loginStatus2 = Preferences.Get("loginStatus2", "0");
            if (loginStatus == "0" || loginStatus2 == "0")
            {
                Application.Current.MainPage = LoginPage;
            }
            else if (loginStatus == "1")
            {
                Application.Current.MainPage = DonorHomepage;
            }
            else if (loginStatus2 == "1")
            {
                Application.Current.MainPage = Homepage;
            }
            else
            {
                // do nothing
            }*/

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            await Task.Delay(3000);
            await Navigation.PushAsync(new DonorReg());
            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }
        private async void Button2_Clicked(object sender, EventArgs e)
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            await Task.Delay(3000);
            await Navigation.PushAsync(new Beneficiary());
            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                System.Diagnostics.Process.GetCurrentProcess().Kill();
            });

            return true;
        }
    }
}
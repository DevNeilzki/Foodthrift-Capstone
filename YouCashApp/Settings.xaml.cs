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
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            await Navigation.PushAsync(new DonorProfile(UserSaveData.Text));
        }


        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var ans = await DisplayAlert("Question?", "Back to Homepage?", "Yes", "No");
                if (ans == true)
                {
                    await Navigation.PopAsync();
                }
            });

            return true;
        }
    }
}
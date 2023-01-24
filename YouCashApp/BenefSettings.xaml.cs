using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BenefSettings : ContentPage
    {
        FirebaseCampaign firebaseHelper = new FirebaseCampaign();
        public BenefSettings()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            var person = await firebaseHelper.GetPerson2(UserSaveData.Text);
            if (person != null)
            {
                var useracc = person.DatePosted;
                if (DateTime.Compare(Convert.ToDateTime(useracc), Convert.ToDateTime(useracc).AddDays(15)) >= 0)
                {
                    await Navigation.PushAsync(new Campaign());
                }
                else
                {
                    await DisplayAlert("Invalid Attempt", "You cannot create new campaign request at the moment. Enables after 15Days", "OK");
                }
            }
            else
            {
                await Navigation.PushAsync(new Campaign());
            }
           
        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            await Navigation.PushAsync(new BenefProfile(UserSaveData.Text));
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

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DonationManagement());
        }
    }
}
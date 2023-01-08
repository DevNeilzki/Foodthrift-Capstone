using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;
using YouCashApp.Model;
using static Xamarin.Forms.Internals.Profile;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CampaignView : ContentPage
    {
        FirebaseCampaign firebaseHelper = new FirebaseCampaign();
        public CampaignView(CampaignDB data)
        {
            InitializeComponent();
            displaydata(data);
        }

        public async void displaydata(CampaignDB display)
        {
            var text = display.ReqTitle;
            var person = await firebaseHelper.GetPerson(text);
            if (person != null)
            {
                lblTitle.Text = person.ReqTitle.ToString();
                lblTitle1.Text = person.BenefName;
                lblTitle2.Text = person.BenefAdd;
                lblTitle3.Text = person.ItemNeeded;
                lblTitle4.Text = person.Description;
                lblTitle5.Text = person.DateNeeded;
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string selection = lblTitle.Text;
            await Navigation.PushAsync(new DonationForm(selection));
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
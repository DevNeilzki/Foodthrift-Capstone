using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;
using YouCashApp.Model;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CampaignOV : ContentPage
    {
        FirebaseCampaign firebaseHelper = new FirebaseCampaign();
        public CampaignOV(CampaignDB data)
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
            await Navigation.PushAsync(new Campaign());
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
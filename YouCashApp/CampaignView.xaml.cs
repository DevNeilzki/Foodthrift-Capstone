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
        FirebaseComment firebaseHelper2 = new FirebaseComment();
        public CampaignView(CampaignDB data)
        {
            InitializeComponent();
            displaydata(data);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            var allPersons3 = await firebaseHelper2.GetAllComments(UserSaveData2.Text);
            lstPersons.ItemsSource = allPersons3;
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
        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            if(string.IsNullOrEmpty(editComments.Text))
            {
                await DisplayAlert("Warning", "Fill Up all the Details", "OK");
            }
            else
            {
                var allPersons = await firebaseHelper2.GetAllComments(UserSaveData2.Text);
                int counter = allPersons.Count;

                await firebaseHelper2.AddComment(counter, UserSaveData.Text, lblTitle.Text, editComments.Text);
            }
        }

    }
}
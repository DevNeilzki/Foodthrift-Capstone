using Android.Service.Autofill;
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
    public partial class Campaign : ContentPage
    {
        FirebaseCampaign firebaseHelper = new FirebaseCampaign();
        public Campaign()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            var allPersons = await firebaseHelper.GetAllPersons();

            int counter = allPersons.Count;

            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            if (string.IsNullOrEmpty(reqTitle.Text) || string.IsNullOrEmpty(benefName.Text) || string.IsNullOrEmpty(ItemNeeds.Text) || string.IsNullOrEmpty(reqDesc.Text) || string.IsNullOrEmpty(benefAdd.Text))
            {
                await DisplayAlert("Warning", "Fill Up all the Details", "OK");
            }
        else
            {
                if (Application.Current.Properties.ContainsKey("UserName"))
                {
                    UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
                }
                await firebaseHelper.AddPerson( counter ,reqTitle.Text, benefName.Text, ItemNeeds.Text, reqDesc.Text, benefAdd.Text, datePicker.Date.ToString() ,UserSaveData.Text);
                reqTitle.Text = string.Empty;
                benefName.Text = string.Empty;
                ItemNeeds.Text = string.Empty;
                reqDesc.Text = string.Empty;
                benefAdd.Text = string.Empty;
                await DisplayAlert("Success", "Campaign Added Successfully", "OK");
                await Navigation.PopAsync();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
            });

            return true;
        }
    }
}
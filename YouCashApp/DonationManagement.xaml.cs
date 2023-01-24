using Android.Text;
using Firebase.Database;
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
    public partial class DonationManagement : ContentPage
    {
        FirebaseDonationManagement firebaseHelper = new FirebaseDonationManagement();
        public DonationManagement()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPerson(UserSaveData.Text);
            lstPersons.ItemsSource = allPersons;
        }

        async void OnCampaignTapped(object sender, ItemTappedEventArgs e)
        {
            DonationManageDB selection = (DonationManageDB)e.Item;
            await Navigation.PushAsync(new DonationStatus(selection));
        }

    }
}
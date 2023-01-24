using Android.Text;
using Firebase.Auth;
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
    public partial class DonationStatus : ContentPage
    {
        FirebaseDonationManagement firebaseHelper = new FirebaseDonationManagement();
        public DonationStatus(DonationManageDB data)
        {
            InitializeComponent();
            displaydata(data);

        }
        public async void check()
        {
            var person = await firebaseHelper.GetPersons(int.Parse(UserSaveData.Text));
            if (person != null)
            {
                var useracc = person.Status;
                if (useracc == "Accepted" || useracc == "Rejected")
                {
                    btnAccept.IsVisible = false;
                    btnReject.IsVisible = false;
                }
            }
        }

        public async void displaydata(DonationManageDB display)
        {
            int text = display.ID;
            var person = await firebaseHelper.GetPerson(text);
            if (person != null)
            {
                lblTitle.Text = person.Title;
                lblTitle1.Text = person.mess;
                lblTitle4.Text = person.Status;
                UserSaveData.Text = person.ID.ToString();
                UserSaveData2.Text = person.donationID.ToString();
                UserSaveData3.Text = person.Donor;
                UserSaveData4.Text = person.Beneficiary;

                check();
            }
        }

        private async void btnAccept_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdatePerson(int.Parse(UserSaveData2.Text), "Accepted", lblTitle.Text, lblTitle1.Text, int.Parse(UserSaveData2.Text), UserSaveData3.Text, UserSaveData4.Text);
            await DisplayAlert("Success", "Donation has been Accepted!", "OK");
            await Navigation.PopAsync();
        }

        private async void btnReject_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdatePerson(int.Parse(UserSaveData2.Text), "Rejected", lblTitle.Text, lblTitle1.Text, int.Parse(UserSaveData2.Text), UserSaveData3.Text, UserSaveData4.Text);
            await DisplayAlert("Success", "Donation has been Rejected!", "OK");
            await Navigation.PopAsync();
        }
    }
}
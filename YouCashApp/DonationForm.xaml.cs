﻿using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;
using YouCashApp.Model;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonationForm : ContentPage
    {
        FirebaseCampaign firebaseHelper = new FirebaseCampaign();
        FirebaseDonation firebaseHelper2 = new FirebaseDonation();
        FirebaseNotification firebaseHelper3 = new FirebaseNotification();
        public DonationForm(string data)
        {
            InitializeComponent();
            displaydata(data);
            foodType.ItemsSource = new List<string> {
            "Non-Perishable Goods",
            "Perishable Goods"
            };

            transpo.ItemsSource = new List<string> {
            "Pick-Up",
            "Delivery"
            };

            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }

        public async void checkExpiry()
        {
            if (datePicker1.Date <= DateTime.Today)
            {
                await DisplayAlert("Error", "The Food is already Expired!", "OK");
            }
        }
        public async void displaydata(string display)
        {
            var text = display;
            var person = await firebaseHelper.GetPerson(text);
            if (person != null)
            {
                reqtitle.Text = person.ReqTitle;
                ItemsDonate.Text = person.ItemNeeded;
                BeneficiaryName.Text = person.BenefName.ToString();
                CmpgnDesc.Text = person.Description;
                BeneficiaryAddress.Text = person.BenefAdd;
                PostedBy.Text = person.Postedby;
            }
        }
        private void datePicker1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

                if (datePicker1.Date > DateTime.Today)
                {
                    datePicker.MinimumDate = DateTime.Today;
                    datePicker.MaximumDate = datePicker1.Date.AddDays(-1);
                }
                else
                {
                    datePicker.MinimumDate = DateTime.Today;
                    datePicker.MaximumDate = DateTime.Today;
                }
          
               
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                var allPersons = await firebaseHelper3.GetAllPerson();

                int counter = allPersons.Count;


                var statusDonation = "";
                var date = datePicker1.Date.ToString();
            var date2 = datePicker.Date.ToString();
            var transport = transpo.SelectedItem.ToString();
            var type = foodType.SelectedItem.ToString();
              

            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            
                if (string.IsNullOrEmpty(ItemsDonate.Text) || string.IsNullOrEmpty(CmpgnDesc.Text) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(BeneficiaryName.Text) || string.IsNullOrEmpty(BeneficiaryAddress.Text) || string.IsNullOrEmpty(transport))
                {
                    await DisplayAlert("Warning", "Fill Up all the Details", "OK");
                }
                else
                {
                    if (Xamarin.Forms.Application.Current.Properties.ContainsKey("UserName"))
                    {
                        UserSaveData.Text = Xamarin.Forms.Application.Current.Properties["UserName"].ToString();
                    }
                    if(datePicker1.Date < DateTime.Today)
                    {
                        await DisplayAlert("Failure", "Donated Failed Check Your Product's Expiry", "OK");
                        activity.IsEnabled = false;
                        activity.IsRunning = false;
                        activity.IsVisible = false;
                    }
                    else
                    {
                        if(transport != "Delivery")
                        {
                            statusDonation = "For Pickup";
                        }
                        else
                        {
                            statusDonation = "For Delivery";
                        }

                        bool isSave = await firebaseHelper2.AddPerson(ItemsDonate.Text, CmpgnDesc.Text, type, date, BeneficiaryName.Text, BeneficiaryAddress.Text, transport, date2, DateTime.Now.ToString("MM/dd/yyyy"), UserSaveData.Text, statusDonation);
                        if (isSave)
                        {
                            var desc = UserSaveData.Text + " donated to your Donation Request for " + BeneficiaryName.Text;
                            await firebaseHelper3.AddPerson(counter,UserSaveData.Text, desc, PostedBy.Text, DateTime.Now.ToString(), statusDonation);
                            ItemsDonate.Text = string.Empty;
                            BeneficiaryName.Text = string.Empty;
                            CmpgnDesc.Text = string.Empty;
                            BeneficiaryName.Text = string.Empty;
                            await DisplayAlert("Success", "Donated Successfully", "OK");
                            activity.IsEnabled = false;
                            activity.IsRunning = false;
                            activity.IsVisible = false;
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Failure", "Donation Failed", "OK");
                            activity.IsEnabled = false;
                            activity.IsRunning = false;
                            activity.IsVisible = false;
                            await Navigation.PopAsync();
                        }
                    }
                }
            }
            catch(Exception )
            {
                await DisplayAlert("Warning", "FoodType and Transportation Method must not be empty", "OK");
                activity.IsEnabled = false;
                activity.IsRunning = false;
                activity.IsVisible = false;
                await Navigation.PopAsync();
            }
            
            
        }

             protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var ans = await DisplayAlert("Question?", "Back to Homepage?", "Yes", "No");
                if (ans == true)
                {
                    activity.IsEnabled = false;
                    activity.IsRunning = false;
                    activity.IsVisible = false;
                    await Navigation.PopAsync();
                }
            });

            return true;
        }
    }
}
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
        FirebaseHelper firebaseHelper4 = new FirebaseHelper();
        FirebaseDonation firebaseHelper2 = new FirebaseDonation();
        FirebaseNotification firebaseHelper3 = new FirebaseNotification();
        FirebaseDonationManagement firebaseHelper5 = new FirebaseDonationManagement();
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

                var allDonation = await firebaseHelper2.GetAllPersons();

                int count = allDonation.Count;


                var statusDonation = "";
                var date = datePicker1.Date.ToString();
            var date2 = datePicker.Date.ToString();
            var transport = transpo.SelectedItem.ToString();
            var type = foodType.SelectedItem.ToString();
                var time = TimePickerSelector.Time.ToString();



                activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            
                if ( string.IsNullOrEmpty(CmpgnDesc.Text) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(BeneficiaryName.Text) || string.IsNullOrEmpty(BeneficiaryAddress.Text) || string.IsNullOrEmpty(transport))
                {
                    await DisplayAlert("Warning", "Fill Up all the Details", "OK");
                }
                else
                {
                    if (Application.Current.Properties.ContainsKey("UserName"))
                    {
                        UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
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
                            statusDonation = "For Delivery";
                        }
                        else
                        {
                            statusDonation = "For Pickup";
                        }

                        bool isSave = await firebaseHelper2.AddPerson(count, reqtitle.Text, CmpgnDesc.Text, type, date, FoodQty.Text, BeneficiaryName.Text, BeneficiaryAddress.Text, transport, date2,time ,DateTime.Now.ToString("MM/dd/yyyy"), UserSaveData.Text, statusDonation, "Not Yet Set");
                        if (isSave)
                        {
                            var useracc="";
                            var person = await firebaseHelper4.GetPerson(UserSaveData.Text);
                            if (person != null)
                            {
                                useracc = person.UserAcc;
                            }


                            var desc = useracc + " donated to your Donation Request for " + BeneficiaryName.Text;
                            await firebaseHelper3.AddPerson(counter, UserSaveData.Text, desc, PostedBy.Text, DateTime.Now.ToString(), statusDonation);
                            var allPersons1 = await firebaseHelper2.GetAlltotalPersons(UserSaveData.Text, BeneficiaryName.Text);
                            int counter2 = allPersons1.Count;
                            if(counter2 > 1)
                            {
                                var user = await firebaseHelper2.GetPersonFavorites(UserSaveData.Text, BeneficiaryName.Text);
                                if (user == null)
                                {
                                    await firebaseHelper2.AddFavorites(UserSaveData.Text, BeneficiaryName.Text);
                                }
                            }

                            var desc1 = useracc + " wants to donate to your request";
                            await firebaseHelper5.AddPerson(counter,count,reqtitle.Text, UserSaveData.Text, desc1, PostedBy.Text, "Pending");

                            BeneficiaryName.Text = string.Empty;
                            CmpgnDesc.Text = string.Empty;
                            BeneficiaryName.Text = string.Empty;
                            await DisplayAlert("Success", "Donated Successfully", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Failure", "Donation Failed", "OK");
                            await Navigation.PopAsync();
                        }
                    }
                }
            }
            catch(Exception )
            {
                await DisplayAlert("Warning", "FoodType and Transportation Method must not be empty", "OK");
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
                    await Navigation.PopAsync();
                }
            });

            return true;
        }
    }
}
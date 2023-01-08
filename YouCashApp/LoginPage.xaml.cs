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
    public partial class LoginPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnButtonClicked(object sender, EventArgs args)
        {

            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(Passwd.Text))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                //call GetUser function which we define in Firebase helper class    
                var user = await firebaseHelper.GetPerson(email.Text);
                //firebase return null valuse if user data not found in database    
                if (user != null)
                    if ((email.Text == user.Email) && (Passwd.Text == user.Password))
                    {
                        await App.Current.MainPage.DisplayAlert("Login Success", "Welcome to Youcash!", "Ok");
                        //Navigate to Wellcom page after successfuly login    
                        //pass user email to welcom page    

                        if (!App.Current.Properties.ContainsKey("UserName"))
                            App.Current.Properties.Add("UserName", email.Text);
                        App.Current.Properties["UserName"] = email.Text;
                        await App.Current.SavePropertiesAsync();

                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                else
                    await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            await Navigation.PushAsync(new SignUp());
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var ans = await DisplayAlert("Question?", "Back to Login Page?", "Yes", "No");
                if (ans == true)
                {
                    await Task.Delay(3000);
                    await Navigation.PushAsync(new LandPage());
                }
            });

            return true;
        }
    }
}
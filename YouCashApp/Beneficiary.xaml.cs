
using Android.App;
using Android.Views;
using Java.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Beneficiary : ContentPage
    {
        UserAuth _userauth = new UserAuth();
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Beneficiary()
        {
            InitializeComponent();
            Task.Run(AnimateBorder);
            email.Focus();
            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }
        private async void AnimateBorder()
        {

            while (true)
            {
                Border.Opacity = 1;
                await Task.Delay(100);
                Border.Opacity = 0.8;
                await Task.Delay(100);
                Border.Opacity = 0.6;
                await Task.Delay(100);
                Border.Opacity = 0.4;
                await Task.Delay(100);
                Border.Opacity = 0.2;
                await Task.Delay(100);
                Border.Opacity = 0;
                await Task.Delay(100);
                Border.Opacity = 0.2;
                await Task.Delay(100);
                Border.Opacity = 0.4;
                await Task.Delay(100);
                Border.Opacity = 0.6;
                await Task.Delay(100);
                Border.Opacity = 0.8;
                await Task.Delay(100);
                Border.Opacity = 1;
                await Task.Delay(100);
            }

        }


        private async void OnButtonClicked(object sender, EventArgs args)
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(Passwd.Text))
            {
                await DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                try
                {
                    string user = email.Text;
                    string pass = Passwd.Text;
                    string token = await _userauth.SignIn(user, pass);
                    if (!string.IsNullOrEmpty(token))
                    {
                       /* Preferences.Set("loginStatus", "1");
                        Xamarin.Forms.Application.Current.MainPage = Homepage;*/
                        bool isSave = await _userauth.Verified(user, pass);
                        if (isSave)
                        {
                            if (!App.Current.Properties.ContainsKey("UserName"))
                                App.Current.Properties.Add("UserName", user);
                            App.Current.Properties["UserName"] = user;
                            await App.Current.SavePropertiesAsync();

                            await Navigation.PushAsync(new Homepage(user));
                            activity.IsEnabled = false;
                            activity.IsRunning = false;
                            activity.IsVisible = false;
                        }
                        else
                        {
                            await DisplayAlert("Activation", "Please activate first your account", "OK");
                            activity.IsEnabled = false;
                            activity.IsRunning = false;
                            activity.IsVisible = false;
                            email.Text = "";
                            Passwd.Text = "";
                        }
                    }
                }
                catch(Exception ex)
                {
                if (ex.Message.Contains("INVALID_PASSWORD"))
                    {
                        await DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                        activity.IsEnabled = false;
                        activity.IsRunning = false;
                        activity.IsVisible = false;
                        email.Text = "";
                        Passwd.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("WARNING", ex.Message, "OK");
                        activity.IsEnabled = false;
                        activity.IsRunning = false;
                        activity.IsVisible = false;
                        email.Text = "";
                        Passwd.Text = "";
                    }
                }
                
            }
                /*  //call GetUser function which we define in Firebase helper class    
                 var user = await firebaseHelper.GetPerson(email.Text);
                 //firebase return null valuse if user data not found in database    
                 if (user != null)
                     if ((email.Text == user.Email) && (Passwd.Text == user.Password))
                     {
                         await App.Current.MainPage.DisplayAlert("Login Success", "Welcome to Foodthrift!", "Ok");
                         //Navigate to Wellcom page after successfuly login    
                         //pass user email to welcom page    
                         await Navigation.PushAsync(new Homepage());
                     }
                     else
                         await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                 else
                     await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");*/
            
           }

            private async void Button_Clicked(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            await Navigation.PushAsync(new SignUp());
            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
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

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPass2());
            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }
    }
}
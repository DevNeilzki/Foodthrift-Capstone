using Android.App;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;
using static Android.Provider.SyncStateContract;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonorReg : ContentPage
    {
        DonorAuth _userauth = new DonorAuth();
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public DonorReg()
        {
            InitializeComponent();
            Task.Run(AnimateBorder);
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
            string user = email.Text;
            string pass = Passwd.Text;
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            var Homepage = new NavigationPage(new DonorHomepage(user));
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(Passwd.Text))
            {
                await DisplayAlert("Empty Values", "Please enter Email and Password", "OK");

            }
            else
            {
                try
                {
                    string token = await _userauth.SignIn(user, pass);
                    if (!string.IsNullOrEmpty(token))
                    {
                        bool isSave = await _userauth.Verified(user, pass);
                        if (isSave)
                        {
                            /*  Preferences.Set("loginStatus", "1");
                              Xamarin.Forms.Application.Current.MainPage = Homepage;
                              await SecureStorage.SetAsync(Constants.UserIdKey, user);*/

                            if (!App.Current.Properties.ContainsKey("UserName"))
                                App.Current.Properties.Add("UserName", email.Text);
                            App.Current.Properties["UserName"] = email.Text;
                            await App.Current.SavePropertiesAsync();

                            await Navigation.PushAsync(new DonorHomepage(user));
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
                catch (Exception ex)
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
            /* //call GetUser function which we define in Firebase helper class    
             var user = await firebaseHelper.GetPerson(email.Text);
             //firebase return null valuse if user data not found in database    
             if (user != null)
                 if ((email.Text == user.Email) && (Passwd.Text == user.Password))
                 {
                     await App.Current.MainPage.DisplayAlert("Login Success", "Welcome to Foodthrift!", "Ok");
                     //Navigate to Wellcom page after successfuly login    
                     //pass user email to welcom page    
                     await Navigation.PushAsync(new DonorHomepage());
                 }
                 else
                     await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
             else
                 await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");*/
        }
        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            await Navigation.PushAsync(new DonorSignUp());
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
  activity.IsEnabled = false;
                        activity.IsRunning = false;
                        activity.IsVisible = false;
                }
            });

            return true;
        }

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPass());
  activity.IsEnabled = false;
                        activity.IsRunning = false;
                        activity.IsVisible = false;
        }
    }
}
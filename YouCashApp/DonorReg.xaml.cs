using Android.App;
using Android.Content.Res;
using Android.Media;
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
using static Android.Provider.ContactsContract.CommonDataKinds;
using static Android.Provider.SyncStateContract;
using static Android.Service.Voice.VoiceInteractionSession;

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
                           
                            var useracc = await firebaseHelper.GetPerson2(user);

                            if (useracc != null)
                                if ((email.Text == useracc.Email) && (Passwd.Text == useracc.Password) && (useracc.Status == "Active"))
                                {
                                    await App.Current.MainPage.DisplayAlert("Login Success", "Welcome to Foodthrift!", "Ok");

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
                                    await App.Current.MainPage.DisplayAlert("Login Fail", "Account is Disabled", "OK");
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
             var useracc = await firebaseHelper.GetPerson(email.Text);
             //firebase return null valuse if user data not found in database    
           */
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
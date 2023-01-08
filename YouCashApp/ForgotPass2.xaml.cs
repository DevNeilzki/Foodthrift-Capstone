using Android.App;
using Firebase.Auth;
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
    public partial class ForgotPass2 : ContentPage
    {
        UserAuth _userauth = new UserAuth();
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ForgotPass2()
        {
            InitializeComponent();
        }


        private async void OnButtonClicked(object sender, EventArgs args)
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            var user = email.Text;

            if (string.IsNullOrEmpty(user))
            {
                await DisplayAlert("WARNING", "Please Enter your Email", "OK");
                return;
            }
            bool isSend = await _userauth.resetPass(user);
            if (isSend)
            {
                await DisplayAlert("Password Reset", "Reset Password link has been sent to your email", "OK");
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
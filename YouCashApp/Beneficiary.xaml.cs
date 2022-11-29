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
    public partial class Beneficiary : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Beneficiary()
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
                        await App.Current.MainPage.DisplayAlert("Login Success", "Welcome to Foodthrift!", "Ok");
                        //Navigate to Wellcom page after successfuly login    
                        //pass user email to welcom page    
                        await Navigation.PushAsync(new Homepage());
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
                var ans = await DisplayAlert("Question?", "Back to Homepage?", "Yes", "No");
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
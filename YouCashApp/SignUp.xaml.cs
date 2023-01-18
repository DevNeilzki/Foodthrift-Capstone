using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;
using YouCashApp.Model;
using System.Net.Mail;
using Firebase.Auth;
using Android.App;


namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SignUp : ContentPage
    {
        UserAuth _userauth = new UserAuth();
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public SignUp()
        {
            InitializeComponent();

            Acctype.ItemsSource = new List<string> {
            "Individual",
            "Organization"
            };

            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            var acctype = Acctype.SelectedItem.ToString();
            var email = txtId.Text;
            var password = txtName.Text;

            if ((!string.IsNullOrEmpty(email)) || (!string.IsNullOrEmpty(password)))
            {
                if (password.Length < 8)
                {
                    await DisplayAlert("Registration Failure", "Password must be at least 8 to 15 characters in length", "OK");
                    txtName.Text = "";
                }
                else
                {
                    try
                    {
                        bool isSave = await _userauth.Register(email, password);
                        if (isSave)
                        {
                            activity.IsEnabled = true;
                            activity.IsRunning = true;
                            activity.IsVisible = true;
                            await DisplayAlert("Registration Success", "Check Your Email to Activate your Account", "OK");
                            await firebaseHelper.AddPerson(txtId.Text, txtName.Text, txtUser.Text, "Not Set Yet", "Not Set Yet", "Not Set Yet", "Not Set Yet", "Basic User", acctype, "Not Set Yet", "Not Set Yet", "Not Set Yet", "Not Set Yet", "Yes", "Active");
                            txtId.Text = string.Empty;
                            txtName.Text = string.Empty;
                            txtUser.Text = string.Empty;
                            activity.IsEnabled = false;
                            activity.IsRunning = false;
                            activity.IsVisible = false;
                            await Navigation.PopAsync();


                        }
                        else
                        {
                            await DisplayAlert("Registration Failure", "Account Registration Failed", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("EMAIL_EXISTS"))
                        {
                            await DisplayAlert("WARNING", "Email Already Exists", "OK");
                            txtName.Text = "";
                            txtId.Text = "";
                        }
                        else if (ex.Message.Contains("INVALID_EMAIL"))
                        {
                            await DisplayAlert("WARNING", "Enter a valid Email Address", "OK");
                            txtName.Text = "";
                            txtId.Text = "";
                        }
                        else
                        {
                            await DisplayAlert("WARNING", ex.Message, "OK");
                            txtName.Text = "";
                            txtId.Text = "";
                        }
                    }

                }
            }
            else
            {
                await DisplayAlert("Warning", "Fill Up all the Details", "OK");
            }

            //await firebaseHelper.AddPerson(txtId.Text, txtName.Text);
           // txtId.Text = string.Empty;
            //txtName.Text = string.Empty;
           // await DisplayAlert("Success", "Person Added Successfully", "OK");
           // var allPersons = await firebaseHelper.GetAllPersons();
           // lstPersons.ItemsSource = allPersons;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var ans = await DisplayAlert("Question?", "Back to Login Page?", "Yes", "No");
                if (ans == true)
                {
                    await Navigation.PopAsync();
                }
            });

            return true;


        }
    }
}
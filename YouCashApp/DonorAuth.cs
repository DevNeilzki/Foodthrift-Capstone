using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using YouCashApp.Helper;
using YouCashApp.Model;

namespace YouCashApp
{
    public class DonorAuth
    {
        static string webAPIkey = "AIzaSyAkuJefOkeHSpcvMRdpgDQ228X3HQKYSqY";
        FirebaseAuthProvider authProvider;

        public DonorAuth()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
        }

        public async Task<bool> Register(string email, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            var content = await token.GetFreshAuthAsync();
            var serializedcontnet = JsonConvert.SerializeObject(content);
            Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);

            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                await authProvider.SendEmailVerificationAsync(token);
                return true;
                /* if (content.User.IsEmailVerified == false)
                 {
                   //  var action = await App.Current.MainPage.DisplayAlert("Alert", "Your Email not activated,Do You want to Send Activation Code again?!", "Yes", "No");


                 else
                 {

                 }*/
            }
            return false;
        }

        public async Task<string> SignIn(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }

        public async Task<bool> Verified(string email, string password)
        {
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            string gettoken = auth.FirebaseToken;
            var content = await auth.GetFreshAuthAsync();
            var serializedcontnet = JsonConvert.SerializeObject(content);
            Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
            if (content.User.IsEmailVerified == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> resetPass(string email)
        {
            await authProvider.SendPasswordResetEmailAsync(email);
            return true;

        }
    }
}

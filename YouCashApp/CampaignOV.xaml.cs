using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;
using YouCashApp.Model;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CampaignOV : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public CampaignOV(Person data)
        {
            InitializeComponent();
            displaydata(data);
        }

        public async void displaydata(Person display)
        {
            var text = display.Email;
            var person = await firebaseHelper.GetPerson(text);
            if (person != null)
            {
                lblTitle.Text = person.Email.ToString();
                lblTitle2.Text = person.Password;
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Campaign());
        }
    }
}
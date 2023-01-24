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
    public partial class Favorites : ContentPage
    {
        FirebaseDonation firebaseHelper2 = new FirebaseDonation();
        public Favorites(string user)
        {

            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            base.OnAppearing();
            var allPersons = await firebaseHelper2.GetAllPersonFavoritesuser(UserSaveData.Text);
            lstPersons.ItemsSource = allPersons;
        }
    }
}
using Java.Nio.Channels;
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
    public partial class Homepage : TabbedPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Homepage(string user)
        {
            InitializeComponent();
            this.Children.Add(new Home() { Title = "Home", Icon = "Homepage.png" });
            this.Children.Add(new BenifNotif() { Title = "Notification", Icon = "Notifications.png" });
            this.Children.Add(new BenefSettings() { Title = "Settings", Icon = "Sett.png" });


            checkprofile(user);
        }

        public async void checkprofile(string user)
        {
            var status = await firebaseHelper.GetPerson(user);
            if (status != null)
            {
                if (status.FirstLogin == "Yes")
                {
                    await Navigation.PushAsync(new BenefProfile(user));
                }
            }
        }
    }
}

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
    public partial class DonorHomepage : TabbedPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public DonorHomepage(string user)
        {
            InitializeComponent();
            this.Children.Add(new DonorHome() { Title = "Home", Icon = "Homepage.png" });
            this.Children.Add(new DonorNotifications() { Title = "Notifications", Icon = "Notifications.png" });
            this.Children.Add(new Settings() { Title = "Settings", Icon = "Sett.png" });
            checkprofile(user);
        }
        public async void checkprofile(string user)
        {
            var status = await firebaseHelper.GetDonor(user);
            if (status != null)
            {
                if (status.FirstLogin == "Yes")
                {
                    await Navigation.PushAsync(new DonorProfile(user));
                }
            }
        }
    }
}

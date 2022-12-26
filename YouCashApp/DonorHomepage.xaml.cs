
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonorHomepage : TabbedPage
    {
        public DonorHomepage()
        {
            InitializeComponent();
            this.Children.Add(new DonorHome() { Title = "Home", Icon = "Homepage.png" });
            this.Children.Add(new DonorNotifications() { Title = "Notifications", Icon = "Notifications.png" });
            this.Children.Add(new Settings() { Title = "Settings", Icon = "Sett.png" });
        }
    }
}

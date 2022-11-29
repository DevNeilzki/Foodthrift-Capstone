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
    public partial class Homepage : TabbedPage
    {
        public Homepage()
        {
            InitializeComponent();
            this.Children.Add(new Home() { Title = "Home", Icon = "Homepage.png" });
            this.Children.Add(new LoginPage() { Title = "Lists", Icon = "Listicon.png" });
            this.Children.Add(new SignUp() { Title = "Notification", Icon = "Notifications.png" });
        }
    }
}
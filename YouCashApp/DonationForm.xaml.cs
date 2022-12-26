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
    public partial class DonationForm : ContentPage
    {
        public DonationForm()
        {
            InitializeComponent();

            foodType.ItemsSource = new List<string> {
            "Non-Perishable Goods",
            "Perishable Goods"
            };

            transpo.ItemsSource = new List<string> {
            "Pick-Up",
            "Delivery"
            };
        }
    }
}
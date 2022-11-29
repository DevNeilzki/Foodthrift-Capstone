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
    public partial class LandPage : ContentPage
    {
        public LandPage()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            await Navigation.PushAsync(new DonorReg());
        }
        private async void Button2_Clicked(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            await Navigation.PushAsync(new Beneficiary());
        }
    }
}
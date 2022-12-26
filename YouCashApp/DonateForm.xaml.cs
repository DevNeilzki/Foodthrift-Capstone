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
    public partial class DonateForm : ContentPage
    {
        public DonateForm()
        {
            InitializeComponent();
          
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var text = title.Text;
            await Navigation.PushAsync(new displaypage(text));
        }
    }
}
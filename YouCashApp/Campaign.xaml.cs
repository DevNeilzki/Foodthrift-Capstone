using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Campaign : ContentPage
    {
        public Campaign()
        {
            InitializeComponent();

            var text = datePicker.Date.ToString("MM/dd/yyyy");
            CmpgnStartDate.Text = text;

            var endDate = DateTime.Now.AddDays(+14).ToString("MM/dd/yyyy");
            CmpgnEndDate.Text = endDate;
        }
    }
}
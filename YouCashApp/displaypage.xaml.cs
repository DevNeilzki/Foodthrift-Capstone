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
    public partial class displaypage : ContentPage
    {
        public displaypage(string displayTitle)
        {
            InitializeComponent();
            display.Text = displayTitle;
        }
    }
}
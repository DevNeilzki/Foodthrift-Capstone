using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YouCashApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                var data = Application.Current.Properties["UserName"].ToString();
                LblDIsplay.Text = data;
            }

            this.BindingContext = this;
        }
        public List<Cash> CashList { get => CashData(); }

        private List<Cash> CashData()
        {
            var tempList = new List<Cash>();
            tempList.Add(new Cash { Money = "-2100", Date = "Sunday 16", Content = "Bank Transfer", Icon = "bank.PNG" });
            tempList.Add(new Cash { Money = "-500", Date = "Monday 17", Content = "Send Money", Icon = "send.PNG" });
            tempList.Add(new Cash { Money = "+150", Date = "Tuesday 18", Content = "Savings", Icon = "savings.PNG" });
            tempList.Add(new Cash { Money = "-360", Date = "Wednesday 19", Content = "Send Money", Icon = "send.PNG" });
            tempList.Add(new Cash { Money = "-1500", Date = "Thursday 20", Content = "Send Money", Icon = "send.PNG" });
            tempList.Add(new Cash { Money = "+5000", Date = "Friday 21", Content = "Cash In", Icon = "cashin.png" });

            return tempList;
        }
        
        protected override bool  OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var ans = await DisplayAlert("Question?", "Would you wish to logout?", "Yes", "No");
                if (ans == true)
                {
                    await Task.Delay(3000);
                    await Navigation.PushAsync(new LoginPage());
                }
            });

            return true;
            
            
        }

    }
    public class Cash
    {
        public string Money { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        public string Icon { get; set; }
    }

    
}

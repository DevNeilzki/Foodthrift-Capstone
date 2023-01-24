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
    public partial class DonorNotifications : ContentPage
    {
        FirebaseNotification firebaseHelper = new FirebaseNotification();
        public IList<Monkey> Monkeys { get; private set; }
        public DonorNotifications()
        {
            InitializeComponent();
            Monkeys = new List<Monkey>();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }
            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllNotifs(UserSaveData.Text);
            lstPersons.ItemsSource = allPersons;
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Monkey selectedItem = e.SelectedItem as Monkey;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Monkey tappedItem = e.Item as Monkey;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
            });

            return true;
        }
    }
}
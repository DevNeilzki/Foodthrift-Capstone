using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCashApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;
using static Android.Icu.Text.CaseMap;
using Android.Service.Autofill;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        FirebaseCampaign firebaseHelper = new FirebaseCampaign();
        public IList<Monkey> Monkeys { get; private set; }
        public Home()
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
            var allPersons = await firebaseHelper.GetAllPersons(UserSaveData.Text);
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

        async void OnCampaignTapped(object sender, ItemTappedEventArgs e)
        {
            CampaignDB selection = (CampaignDB)e.Item;
            await Navigation.PushAsync(new CampaignOV(selection));
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushAsync(new CampaignOV());//
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                var _lastPage = _navigation.NavigationStack.LastOrDefault();
                await Navigation.PopAsync();
               // await Navigation.PushAsync(new LandPage());

            });

            return true;
        }
    }
}
using Android.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using YouCashApp.Helper;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonorProfile : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public DonorProfile(string user)
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                UserSaveData.Text = Application.Current.Properties["UserName"].ToString();
            }

            Email.Text = UserSaveData.Text;

            displaydata(user);
        }

        public async void displaydata(string display)
        {
            var text = display;
            var person = await firebaseHelper.GetDonor(text);
            if (person != null)
            {
                lblUser.Text = person.UserAcc;
                Pass.Text = person.Password;
                Fname.Text = person.Fname;
                Lname.Text = person.Lname;
                Entryadd.Text = person.Address;
                EntryContct.Text = person.Contact;
                BusName.Text = person.BusinessName;
                BusEmail.Text = person.BusinessEmail;
                BusContact.Text = person.BusinessContact;
                TIN.Text = person.BusinessTIN;
            }
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Fname.IsReadOnly = false;
            Lname.IsReadOnly = false;
            Entryadd.IsReadOnly = false;
            EntryContct.IsReadOnly = false;
            BusName.IsReadOnly = false;
            BusEmail.IsReadOnly = false;
            BusContact.IsReadOnly = false;
            TIN.IsReadOnly = false;
            btnSave.IsVisible = true;
            btnEdit.IsVisible = false;
        }


        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Fname.Text) || string.IsNullOrEmpty(Lname.Text) || 
                string.IsNullOrEmpty(Entryadd.Text) || string.IsNullOrEmpty(EntryContct.Text) || string.IsNullOrEmpty(BusName.Text) ||
                string.IsNullOrEmpty(BusEmail.Text) || string.IsNullOrEmpty(BusContact.Text) || string.IsNullOrEmpty(TIN.Text))
            {
                await DisplayAlert("Warning", "Fill Up all the Details", "OK");
            }
            else
            {
                bool save = await firebaseHelper.UpdateDonor(Email.Text, Pass.Text, lblUser.Text, Fname.Text, Lname.Text, Entryadd.Text, EntryContct.Text, BusName.Text, BusEmail.Text, BusContact.Text, TIN.Text, "No");
                if(save)
                {
                    await DisplayAlert("Success", "Profile Updated Successfully!", "OK");
                    Fname.IsReadOnly = true;
                    Lname.IsReadOnly = true;
                    Entryadd.IsReadOnly = true;
                    EntryContct.IsReadOnly = true;
                    BusName.IsReadOnly = true;
                    BusEmail.IsReadOnly = true;
                    BusContact.IsReadOnly = true;
                    TIN.IsReadOnly = true;
                    btnSave.IsVisible = false;
                    btnEdit.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Failure", "Profile Failed to Update!", "OK");
                }
                
            }
           
        }
    }
}
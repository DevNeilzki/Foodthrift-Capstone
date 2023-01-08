using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCashApp.Model;

namespace YouCashApp.Helper
{
    public class FirebaseDonation
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");
        public async Task<List<DonationDB>> GetAllPersons()
        {
            return (await firebase
              .Child("Donation")
              .OnceAsync<DonationDB>()).Select(item => new DonationDB
              {
                  Item2Donate = item.Object.Item2Donate,
                  Description = item.Object.Description,
                  FoodType = item.Object.FoodType,
                  FoodExpiry = item.Object.FoodExpiry,
                  BeneficiaryName = item.Object.BeneficiaryName,
                  BeneficiaryAdd = item.Object.BeneficiaryAdd,
                  TransportMethod = item.Object.TransportMethod,
                  PickUPDate = item.Object.PickUPDate,
                  DateDonated = item.Object.DateDonated,
                  Donatedby = item.Object.Donatedby,
                  Status = item.Object.Status
              }).ToList();
        }
        public async Task<bool> AddPerson(string item2donate, string desc, string type, string expiry, string benefname, string benefadd, string transpo, string pickUp, string datedonate, string donateby, string status)
        {
            try
            {
                await firebase
             .Child("Donation")
             .PostAsync(new DonationDB() { Item2Donate = item2donate, Description = desc, FoodType = type, FoodExpiry = expiry, BeneficiaryName = benefname, BeneficiaryAdd = benefadd, TransportMethod = transpo, PickUPDate = pickUp, DateDonated = datedonate, Donatedby = donateby, Status = status });
                return true;
            }
            catch(Exception)
            {
                return false;
            }
          
        }
    }
}

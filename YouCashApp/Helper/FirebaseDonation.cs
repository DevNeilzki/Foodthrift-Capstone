using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                  ID = item.Object.ID,
                  Title = item.Object.Title,
                  Description = item.Object.Description,
                  FoodType = item.Object.FoodType,
                  FoodExpiry = item.Object.FoodExpiry,
                  qtyFoods = item.Object.qtyFoods,
                  BeneficiaryName = item.Object.BeneficiaryName,
                  BeneficiaryAdd = item.Object.BeneficiaryAdd,
                  TransportMethod = item.Object.TransportMethod,
                  PickUPDate = item.Object.PickUPDate,
                  PickTime = item.Object.PickTime,
                  DateDonated = item.Object.DateDonated,
                  Donatedby = item.Object.Donatedby,
                  Status = item.Object.Status,
                  DeliverySchedule = item.Object.DeliverySchedule
              }).ToList();
        }


        public async Task<List<FavoritesDB>> GetAllPersonFavorites()
        {
            return (await firebase
              .Child("Favotirites")
              .OnceAsync<FavoritesDB>()).Select(item => new FavoritesDB
              {
                  user = item.Object.user,
                  beneficiary = item.Object.beneficiary
              }).ToList();
        }

        public async Task<List<FavoritesDB>> GetAllPersonFavoritesuser(string user)
        {
            return (await firebase
              .Child("Favorites")
              .OnceAsync<FavoritesDB>()).Select(item => new FavoritesDB
              {
                  user = item.Object.user,
                  beneficiary = item.Object.beneficiary
              }).Where(a => a.user == user).ToList();
        }
        public async Task<List<DonationDB>> GetAlltotalPersons(string donator, string beneficiary)
        {
            return (await firebase
              .Child("Donation")
              .OnceAsync<DonationDB>()).Select(item => new DonationDB
              {
                  Title = item.Object.Title,
                  Description = item.Object.Description,
                  FoodType = item.Object.FoodType,
                  FoodExpiry = item.Object.FoodExpiry,
                  qtyFoods = item.Object.qtyFoods,
                  BeneficiaryName = item.Object.BeneficiaryName,
                  BeneficiaryAdd = item.Object.BeneficiaryAdd,
                  TransportMethod = item.Object.TransportMethod,
                  PickUPDate = item.Object.PickUPDate,
                  PickTime = item.Object.PickTime,
                  DateDonated = item.Object.DateDonated,
                  Donatedby = item.Object.Donatedby,
                  Status = item.Object.Status,
                  DeliverySchedule = item.Object.DeliverySchedule
              }).Where(a => (a.Donatedby == donator) && (a.BeneficiaryName == beneficiary)).ToList();
        }
        public async Task<bool> AddPerson(int id,string title, string desc, string type, string expiry, string qty, string benefname, string benefadd, string transpo, string pickUp, string picktime, string datedonate, string donateby, string status, string deliverystats)
        {
            try
            {
                await firebase
             .Child("Donation")
             .PostAsync(new DonationDB() {ID = id, Title = title, Description = desc, FoodType = type, FoodExpiry = expiry, qtyFoods = qty, BeneficiaryName = benefname, BeneficiaryAdd = benefadd, TransportMethod = transpo, PickUPDate = pickUp, PickTime = picktime, DateDonated = datedonate, Donatedby = donateby, Status = status, DeliverySchedule = deliverystats });
                return true;
            }
            catch(Exception)
            {
                return false;
            }
          
        }

        public async Task<bool> AddFavorites(string sender, string benefy)
        {
            try
            {
                await firebase
             .Child("Favorites")
             .PostAsync(new FavoritesDB() { user = sender, beneficiary = benefy});
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<FavoritesDB> GetPersonFavorites(string sender, string benefy)
        {
            var allPersons = await GetAllPersonFavorites();
            await firebase
              .Child("Favorites")
              .OnceAsync<FavoritesDB>();
            return allPersons.Where(a => a.user == sender && a.beneficiary == benefy).FirstOrDefault();
        }
    }
}

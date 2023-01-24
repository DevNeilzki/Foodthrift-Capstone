using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCashApp.Model;
using static Android.Icu.Text.CaseMap;

namespace YouCashApp.Helper
{
    public class FirebaseDonationManagement
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");

        public async Task<List<DonationManageDB>> GetAllPerson(string target)
        {
            return (await firebase
              .Child("DonationAcceptance")
              .OnceAsync<DonationManageDB>()).Select(item => new DonationManageDB
              {
                  ID = item.Object.ID,
                  donationID = item.Object.donationID,
                  Title = item.Object.Title,
                  Donor = item.Object.Donor,
                  mess = item.Object.mess,
                  Beneficiary = item.Object.Beneficiary,
                  Status = item.Object.Status
              }).Where(a => a.Beneficiary == target).OrderByDescending(user => user.ID).ToList();
        }

        public async Task<List<DonationManageDB>> GetAllPersons()
        {
            return (await firebase
              .Child("DonationAcceptance")
              .OnceAsync<DonationManageDB>()).Select(item => new DonationManageDB
              {
                  ID = item.Object.ID,
                  donationID = item.Object.donationID,
                  Title = item.Object.Title,
                  Donor = item.Object.Donor,
                  mess = item.Object.mess,
                  Beneficiary = item.Object.Beneficiary,
                  Status = item.Object.Status
              }).ToList();
        }

        public async Task UpdateDonation(int id, string status)
        {
            var toUpdatePerson = (await firebase
              .Child("Donation")
              .OnceAsync<DonationDB>()).Where(a => a.Object.ID == id).FirstOrDefault();

            await firebase
              .Child("Donation")
              .Child(toUpdatePerson.Key)
              .PutAsync(new DonationDB() {Status = status });
        }


        public async Task UpdatePerson(int id, string status, string title, string msg, int donID, string donorname, string benef)
        {
            var toUpdatePerson = (await firebase
              .Child("DonationAcceptance")
              .OnceAsync<DonationManageDB>()).Where(a => a.Object.ID == id).FirstOrDefault();

            await firebase
              .Child("DonationAcceptance")
              .Child(toUpdatePerson.Key)
              .PutAsync(new DonationManageDB() {ID = id, Status = status, Title = title, mess = msg, donationID = donID, Donor = donorname, Beneficiary = benef});
        }


        public async Task<DonationManageDB> GetPerson(int id)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("DonationAcceptance")
              .OnceAsync<DonationManageDB>();
            return allPersons.Where(a => a.ID == id).FirstOrDefault();
        }

        public async Task<DonationManageDB> GetPersons(int id)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("DonationAcceptance")
              .OnceAsync<DonationManageDB>();
            return allPersons.Where(a => a.ID == id).FirstOrDefault();
        }

        public async Task AddPerson(int id, int donorID,string title, string donor, string msg, string benef, string stats)
        {
            await firebase
              .Child("DonationAcceptance")
              .PostAsync(new DonationManageDB() { ID = id, donationID = donorID, Title = title, Donor = donor, mess = msg, Beneficiary = benef, Status = stats });
        }
    }
}

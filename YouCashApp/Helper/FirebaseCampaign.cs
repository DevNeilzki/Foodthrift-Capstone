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
    public class FirebaseCampaign
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");
        public async Task<List<CampaignDB>> GetAllPersons()
        {

            return (await firebase
              .Child("Campaigns")
              .OnceAsync<CampaignDB>()).Select(item => new CampaignDB
              {
                  ID = item.Object.ID,
                  ReqTitle = item.Object.ReqTitle,
                  BenefName = item.Object.BenefName,
                  ItemNeeded = item.Object.ItemNeeded,
                  Description = item.Object.Description,
                  BenefAdd = item.Object.BenefAdd,
                  DateNeeded = item.Object.DateNeeded,
                  Postedby = item.Object.Postedby
              }).OrderByDescending(user => user.ID).ToList();
        }

        public async Task AddPerson(int id, string reqtitle, string benefname, string itemneed, string desc, string benefadd, string dateneed, string postby)
        {

            await firebase
              .Child("Campaigns")
              .PostAsync(new CampaignDB() { ID = id, ReqTitle = reqtitle, BenefName = benefname, ItemNeeded = itemneed, Description = desc, BenefAdd = benefadd, DateNeeded = dateneed, Postedby = postby });
        }

        public async Task<CampaignDB> GetPerson(string title)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Campaigns")
              .OnceAsync<CampaignDB>();
            return allPersons.Where(a => a.ReqTitle == title).FirstOrDefault();
        }
    }
}

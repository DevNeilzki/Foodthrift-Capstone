using Firebase.Database;
using Firebase.Database.Query;
using Java.Lang;
using Java.Lang.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCashApp.Model;
using static Android.Provider.ContactsContract.Intents;

namespace YouCashApp.Helper
{
    public class FirebaseCampaign
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");
        public async Task<List<CampaignDB>> GetAllPersons(string author)
        {

            return (await firebase
              .Child("Campaigns")
              .OnceAsync<CampaignDB>()).Select(item => new CampaignDB
              {
                  ID = item.Object.ID,
                  ReqTitle = item.Object.ReqTitle,
                  BenefName = item.Object.BenefName,
                  DatePosted = item.Object.DatePosted,
                  Description = item.Object.Description,    
                  BenefAdd = item.Object.BenefAdd,
                  DateNeeded = item.Object.DateNeeded,
                  Postedby = item.Object.Postedby
              }).Where(user => user.Postedby == author).Where(a => DateTime.Compare(Convert.ToDateTime(a.DateNeeded), DateTime.Now) >= 0).OrderByDescending(user => user.ID).ToList();
        }

        public async Task<List<CampaignDB>> GetAllPerson()
        {

            return (await firebase
              .Child("Campaigns")
              .OnceAsync<CampaignDB>()).Select(item => new CampaignDB
              {
                  ID = item.Object.ID,
                  ReqTitle = item.Object.ReqTitle,
                  BenefName = item.Object.BenefName,
                  Description = item.Object.Description,
                  DatePosted = item.Object.DatePosted,
                  BenefAdd = item.Object.BenefAdd,
                  DateNeeded = item.Object.DateNeeded,
                  Postedby = item.Object.Postedby
              }).Where(a => DateTime.Compare(Convert.ToDateTime(a.DateNeeded), DateTime.Now) >= 0).OrderByDescending(user => user.ID).ToList();
        }

        public async Task AddPerson(int id, string reqtitle, string benefname, string desc, string benefadd, string dateneed, string postby, string datepost)
        {

            await firebase
              .Child("Campaigns")
              .PostAsync(new CampaignDB() { ID = id, ReqTitle = reqtitle, BenefName = benefname, Description = desc, BenefAdd = benefadd, DateNeeded = dateneed, Postedby = postby, DatePosted = datepost});
        }

        public async Task<CampaignDB> GetPerson(string title)
        {
            var allPersons = await GetAllPerson();
            await firebase
              .Child("Campaigns")
              .OnceAsync<CampaignDB>();
            return allPersons.Where(a => a.ReqTitle == title).FirstOrDefault();
        }
        public async Task<CampaignDB> GetPerson2(string title)
        {
            var allPersons = await GetAllPerson();
            await firebase
              .Child("Campaigns")
              .OnceAsync<CampaignDB>();
            return allPersons.Where(a => a.Postedby == title).FirstOrDefault();
        }
    }
}

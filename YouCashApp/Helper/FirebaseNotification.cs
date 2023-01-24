using Firebase.Database;
using Firebase.Database.Query;
using Java.Lang.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YouCashApp.Model;

namespace YouCashApp.Helper
{
    public class FirebaseNotification
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");

        public async Task<List<NotifDB>> GetAllPerson()
        {
            return (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Select(item => new NotifDB
              {
                  ID = item.Object.ID,
                  Sender = item.Object.Sender,
                  Description = item.Object.Description,
                  Target = item.Object.Target,
                  Date = item.Object.Date,
                  Status = item.Object.Status
              }).ToList();
        }
        public async Task<List<NotifDB>> GetAllPersons(string target)
        {
            return (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Select(item => new NotifDB
              {
                  ID = item.Object.ID,
                  Sender = item.Object.Sender,
                  Description = item.Object.Description,
                  Target = item.Object.Target,
                  Date = item.Object.Date,
                  Status = item.Object.Status
              }).Where(a => a.Target == target).ToList();
        }

        public async Task<List<NotifDB>> GetAllNotifs(string sender)
        {
            return (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Select(item => new NotifDB
              {
                  ID = item.Object.ID,
                  Sender = item.Object.Sender,
                  Description = item.Object.Description,
                  Target = item.Object.Target,
                  Date = item.Object.Date,
                  Status = item.Object.Status
              }).Where(a => a.Sender == sender).ToList();
        }

        public async Task AddPerson(int id, string sender, string desc, string targt, string date, string stats)
        {
            await firebase
              .Child("Notification")
              .PostAsync(new NotifDB() { ID = id, Sender = sender, Description = desc, Target = targt, Date = date, Status = stats });
        }



        public async Task UpdatePerson(int id, string sender, string desc, string targt, string date)
        {
            var toUpdatePerson = (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Where(a => a.Object.Sender == sender).FirstOrDefault();

            await firebase
              .Child("Notification")
              .Child(toUpdatePerson.Key)
              .PutAsync(new NotifDB() { ID = id, Sender = sender, Description = desc, Target = targt, Date = date });
        }

        public async Task DeletePerson(string sender)
        {
            var toDeletePerson = (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Where(a => a.Object.Sender == sender).FirstOrDefault();
            await firebase.Child("Notification").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}

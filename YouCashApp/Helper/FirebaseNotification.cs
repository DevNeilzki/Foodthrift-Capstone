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
        public async Task<List<NotifDB>> GetAllPersons(string target)
        {
            return (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Select(item => new NotifDB
              {
                  Sender = item.Object.Sender,
                  Description = item.Object.Description,
                  Target = item.Object.Target,
                  Date = item.Object.Date
              }).Where(a => a.Target == target).ToList();
        }

        public async Task<List<NotifDB>> GetAllNotifs(string sender)
        {
            return (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Select(item => new NotifDB
              {
                  Sender = item.Object.Sender,
                  Description = item.Object.Description,
                  Target = item.Object.Target,
                  Date = item.Object.Date
              }).Where(a => a.Sender == sender).ToList();
        }

        public async Task AddPerson(string sender, string desc, string targt, string date)
        {

            await firebase
              .Child("Notification")
              .PostAsync(new NotifDB() { Sender = sender, Description = desc, Target = targt, Date = date });
        }



        public async Task UpdatePerson(string sender, string desc, string targt, string date)
        {
            var toUpdatePerson = (await firebase
              .Child("Notification")
              .OnceAsync<NotifDB>()).Where(a => a.Object.Sender == sender).FirstOrDefault();

            await firebase
              .Child("Notification")
              .Child(toUpdatePerson.Key)
              .PutAsync(new NotifDB() { Sender = sender, Description = desc, Target = targt, Date = date });
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

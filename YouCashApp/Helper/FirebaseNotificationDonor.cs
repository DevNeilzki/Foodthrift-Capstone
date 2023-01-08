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
    public class FirebaseNotificationDonor
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");
        public async Task<List<NotifDBDonor>> GetAllPersons(string target)
        {
            return (await firebase
              .Child("NotificationDonor")
              .OnceAsync<NotifDBDonor>()).Select(item => new NotifDBDonor
              {
                  Sender = item.Object.Sender,
                  Description = item.Object.Description,
                  Target = item.Object.Target,
                  Date = item.Object.Date
              }).Where(a => a.Target == target).ToList();
        }

        public async Task<List<NotifDBDonor>> GetAllNotifs(string sender)
        {
            return (await firebase
              .Child("NotificationDonor")
              .OnceAsync<NotifDBDonor>()).Select(item => new NotifDBDonor
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
              .Child("NotificationDonor")
              .PostAsync(new NotifDBDonor() { Sender = sender, Description = desc, Target = targt, Date = date });
        }



        public async Task UpdatePerson(string sender, string desc, string targt, string date)
        {
            var toUpdatePerson = (await firebase
              .Child("NotificationDonor")
              .OnceAsync<NotifDBDonor>()).Where(a => a.Object.Sender == sender).FirstOrDefault();

            await firebase
              .Child("NotificationDonor")
              .Child(toUpdatePerson.Key)
              .PutAsync(new NotifDBDonor() { Sender = sender, Description = desc, Target = targt, Date = date });
        }

        public async Task DeletePerson(string sender)
        {
            var toDeletePerson = (await firebase
              .Child("NotificationDonor")
              .OnceAsync<NotifDBDonor>()).Where(a => a.Object.Sender == sender).FirstOrDefault();
            await firebase.Child("NotificationDonor").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using YouCashApp.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YouCashApp.Helper
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");
        public async Task<List<Person>> GetAllPersons()
        {

            return (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Select(item => new Person
              {
                  Email = item.Object.Email,
                  Password = item.Object.Password
              }).ToList();
        }
        public async Task AddPerson(string email, string password)
        {

            await firebase
              .Child("Persons")
              .PostAsync(new Person() { Email = email, Password = password });
        }

        public async Task<Person> GetPerson(string email)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<Person>();
            return allPersons.Where(a => a.Email == email).FirstOrDefault();
        }

        public async Task UpdatePerson(string email, string password)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.Email == email).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Person() { Email = email, Password = password });
        }

        public async Task DeletePerson(string email)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.Email == email).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}

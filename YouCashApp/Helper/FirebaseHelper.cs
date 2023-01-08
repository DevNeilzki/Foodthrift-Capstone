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
                  Password = item.Object.Password,
                  UserAcc = item.Object.UserAcc,
                  Fname = item.Object.Fname,
                  Lname = item.Object.Lname,
                  Address = item.Object.Address,
                  Contact = item.Object.Contact,
                  Subscription = item.Object.Subscription,
                  AccType = item.Object.AccType,
                  BusinessName = item.Object.BusinessName,
                  BusinessEmail = item.Object.BusinessEmail,
                  BusinessContact = item.Object.BusinessContact,
                  BusinessTIN = item.Object.BusinessTIN,
                  FirstLogin = item.Object.FirstLogin

              }).ToList();
        }

        public async Task<List<Person>> GetAllDonor()
        {

            return (await firebase
              .Child("Donor")
              .OnceAsync<Person>()).Select(item => new Person
              {
                  Email = item.Object.Email,
                  Password = item.Object.Password,
                  UserAcc = item.Object.UserAcc,
                  Fname = item.Object.Fname,
                  Lname = item.Object.Lname,
                  Address = item.Object.Address,
                  Contact = item.Object.Contact,
                  BusinessName = item.Object.BusinessName,
                  BusinessEmail = item.Object.BusinessEmail,
                  BusinessContact = item.Object.BusinessContact,
                  BusinessTIN = item.Object.BusinessTIN,
                  FirstLogin = item.Object.FirstLogin

              }).ToList();
        }
        public async Task AddPerson(string email, string password, string useracc, string fname, string lname, string add, string cntct, string subs, string acctype, string busname, string busemail, string buscontct, string busTIN, string firstlog)
        {

            await firebase
              .Child("Persons")
              .PostAsync(new Person() { Email = email, Password = password, UserAcc = useracc, Fname = fname, Lname = lname, Address = add, Contact = cntct, Subscription = subs, AccType = acctype, BusinessName = busname, BusinessEmail = busemail, BusinessContact = buscontct, BusinessTIN = busTIN, FirstLogin = firstlog });
        }

         public async Task AddDonor(string email, string password, string useracc, string fname, string lname, string add, string cntct, string busname, string busemail, string buscontct, string busTIN, string firstlog)
        {

            await firebase
              .Child("Donor")
              .PostAsync(new Person() { Email = email, Password = password, UserAcc = useracc, Fname = fname, Lname = lname, Address = add, Contact = cntct, BusinessName = busname, BusinessEmail = busemail, BusinessContact = buscontct, BusinessTIN = busTIN, FirstLogin = firstlog });
        }

        public async Task<Person> GetPerson(string email)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<Person>();
            return allPersons.Where(a => a.Email == email).FirstOrDefault();
        }

        public async Task<bool> UpdatePerson(string email, string password, string useracc, string fname, string lname, string add, string cntct, string subs, string acctype, string busname, string busemail, string buscontct, string busTIN, string firstlog)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.Email == email).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Person() { Email = email, Password = password, UserAcc = useracc, Fname = fname, Lname = lname, Address = add, Contact = cntct, Subscription = subs, AccType = acctype, BusinessName = busname, BusinessEmail = busemail, BusinessContact = buscontct, BusinessTIN = busTIN, FirstLogin = firstlog });
            return true;
        }

        public async Task<Person> GetDonor(string email)
        {
            var allPersons = await GetAllDonor();
            await firebase
              .Child("Donor")
              .OnceAsync<Person>();
            return allPersons.Where(a => a.Email == email).FirstOrDefault();
        }

        public async Task<bool> UpdateDonor(string email, string password, string useracc, string fname, string lname, string add, string cntct, string busname, string busemail, string buscontct, string busTIN, string firstlog)
        {
            var toUpdatePerson = (await firebase
              .Child("Donor")
              .OnceAsync<Person>()).Where(a => a.Object.Email == email).FirstOrDefault();

            await firebase
              .Child("Donor")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Person() { Email = email, Password = password, UserAcc = useracc, Fname = fname, Lname = lname, Address = add, Contact = cntct, BusinessName = busname, BusinessEmail = busemail, BusinessContact = buscontct, BusinessTIN = busTIN, FirstLogin = firstlog });
            return true;
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

using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using YouCashApp.Model;

namespace YouCashApp.Helper
{
    public class FirebaseComment
    {
        FirebaseClient firebase = new FirebaseClient("https://youcash-9427e-default-rtdb.firebaseio.com/");
        public async Task<List<CommendDB>> GetAllComments(string titles)
        {
            return (await firebase
              .Child("Comments")
              .OnceAsync<CommendDB>()).Select(item => new CommendDB
              {
                  ID = item.Object.ID,
                  Email = item.Object.Email,
                  CmpngTitle = item.Object.CmpngTitle,
                  Message = item.Object.Message
              }).Where(a => a.CmpngTitle == titles).ToList();
        }

        public async Task AddComment(int id, string email, string title, string mess)
        {
            await firebase
              .Child("Comments")
              .PostAsync(new CommendDB() { ID = id, Email = email, CmpngTitle = title, Message = mess});
        }

    }
}

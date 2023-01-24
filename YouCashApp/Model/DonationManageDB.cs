using System;
using System.Collections.Generic;
using System.Text;

namespace YouCashApp.Model
{
    public class DonationManageDB
    {
        public int ID { get; set; }
        public int donationID { get; set; }
        public string Title { get; set; }
        public string Donor { get; set; }
        public string mess { get; set; }
        public string Beneficiary { get; set; }
        public string Status { get; set; }
    }
}

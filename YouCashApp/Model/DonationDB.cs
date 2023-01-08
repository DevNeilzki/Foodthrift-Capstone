using System;
using System.Collections.Generic;
using System.Text;

namespace YouCashApp.Model
{
    public class DonationDB
    {
        public string Item2Donate { get; set; }
        public string Description { get; set; }
        public string FoodType { get; set; }
        public string FoodExpiry { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAdd { get; set; }
        public string TransportMethod { get; set; }
        public string PickUPDate { get; set; }

        public string DateDonated { get; set; }
        public string Donatedby { get; set; }

        public string Status { get; set; }

    }
}

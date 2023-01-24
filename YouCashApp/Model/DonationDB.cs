using System;
using System.Collections.Generic;
using System.Text;

namespace YouCashApp.Model
{
    public class DonationDB
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FoodType { get; set; }
        public string FoodExpiry { get; set; }
        public string qtyFoods { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAdd { get; set; }
        public string TransportMethod { get; set; }
        public string PickUPDate { get; set; }
        public string PickTime { get; set; }
        public string DateDonated { get; set; }
        public string Donatedby { get; set; }
        public string Status { get; set; }
        public string DeliverySchedule { get; set; }
        

    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YouCashApp.Model
{
   public class NotifDB
    {
        public int ID { get; set; }
        public string Sender { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }

    }
}

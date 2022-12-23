﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YouCashApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Homepage : TabbedPage
    {
        public Homepage()
        {
            InitializeComponent();
            this.Children.Add(new Home() { Title = "Home", Icon = "Homepage.png" });
            this.Children.Add(new LoginPage() { Title = "Incoming Donations", Icon = "Listicon.png" });
            this.Children.Add(new Settings() { Title = "Notification", Icon = "Notifications.png" });
            this.Children.Add(new Settings() { Title = "Settings", Icon = "Notifications.png" });

        }
    }
}
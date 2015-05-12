using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RedditPhone
{
    public partial class UserPage : PhoneApplicationPage
    {
        public UserPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("key"))
            {
                string val = NavigationContext.QueryString["key"];
                txtUserPage.Text = "Welcome " + val;
            }
            if (NavigationContext.QueryString.ContainsKey("comments"))
            {
                string val1 = NavigationContext.QueryString["comments"];
                txtInfo.Text = "Total comments: " + val1;
            }
        }


    }
}
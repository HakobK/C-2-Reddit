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
            string name = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("name", out name))
            {
                txtUserPage.Text = name;
            }
        }



    }
}
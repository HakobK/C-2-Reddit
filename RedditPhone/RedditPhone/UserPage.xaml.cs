using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RedditPhone.Resources;
using System.Threading.Tasks;

namespace RedditPhone
{
    public partial class UserPage : PhoneApplicationPage
    {
        MainPage authentication = new MainPage();
        public UserPage()
        {
            InitializeComponent();

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var sReddit = await Task.Factory.StartNew(() => { return authentication.authenticatedReddit.User.FullName; });
            txtUserPage.Text = sReddit;

            //if (NavigationContext.QueryString.ContainsKey("key"))
            //{
            //    string val = NavigationContext.QueryString["key"];
            //    txtUserPage.Text = "Welcome " + val;
            //}
            //if (NavigationContext.QueryString.ContainsKey("comments"))
            //{
            //    string listing = "";
            //    var val1 = NavigationContext.QueryString["comments"];
            //    foreach(object s in val1)
            //    {
            //        listing = listing + " " + s.ToString();
            //    }
            //    txtInfo.Text = "Total comments: " + listing;
            //}
            //if (NavigationContext.QueryString.ContainsKey("createdat"))
            //{
            //    string val1 = NavigationContext.QueryString["createdat"];

            //    createdat.Text = "User create date: " + val1;
            //}


            
        }


    }
}
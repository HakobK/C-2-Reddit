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
using Newtonsoft;
using RedditSharpPCL;
using System.Threading.Tasks;

namespace RedditPhone
{

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();


            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }



        private async void lgnReddit_Click(object sender, RoutedEventArgs e)
        {
            await login(txtUsername.Text, txtPassword.Password);
        }

        private async Task login(string username, string password)
        {
            try
            {
                Reddit reddit = new Reddit();
                RedditUser user = await Task.Factory.StartNew(() => { return reddit.LogIn(username, password); });
                MessageBox.Show(user.Created.ToString());

                // NavigationService.Navigate(new Uri("/UserPage.xaml?name=",UriKind.Relative));


                NavigationService.Navigate(new Uri("/UserPage.xaml?key=" + user.FullName + "&comments=" + user.Posts, UriKind.Relative));
            }
            catch(Exception)
            {
                MessageBox.Show("Invalid login");
            }

            //Subreddit s = await Task.Factory.StartNew(() => { return reddit.GetSubreddit("fatpeoplehate"); });
            //Subreddit f = await Task.Run() => reddit.GetSubreddit("windowsphone")
            //MessageBox.Show(s.Subscribers.ToString());
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}
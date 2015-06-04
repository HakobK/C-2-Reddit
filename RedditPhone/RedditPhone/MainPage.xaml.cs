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

        public Reddit authenticatedReddit;
        public bool loggedIn;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            authenticatedReddit = new Reddit();
            loggedIn = false;
     
            //sAdControl.ErrorOccurred += new EventHandler<Microsoft.Advertising.AdErrorEventArgs>(sAdControl_ErrorOccurred);
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public async Task<Reddit> tst()
        {
           return authenticatedReddit;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.ContainsKey("key"))
            {
                string message = NavigationContext.QueryString["key"];
                if(message == "false")
                {
                    MessageBox.Show("Invalid login");
                }
            }
        }


        private async void lgnReddit_Click(object sender, RoutedEventArgs e)
        {
            await login2(txtUsername.Text, txtPassword.Password);
        }

        public async void dostuff()
        {
            await Task.Factory.StartNew(() =>
                {
                    Dispatcher.BeginInvoke(()=> MessageBox.Show(authenticatedReddit.User.FullName.ToString()));
                });
        }
        public async Task login2(string user, string pass)
        {


            await Task.Factory.StartNew(() =>
            {
                //LoggedInReddit.LogIn(user, pass);

                try
                {
                    authenticatedReddit.LogIn(user,pass);
                    dostuff();
                    loggedIn = true;

                }
                catch (Exception e)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show(e.ToString());

                        //NavigationService.Navigate(new Uri("/MainPage.xaml?key=" + "false", UriKind.Relative));
                    });
                }
            });

        }


        private async Task login(string username, string password)
        {
                
           //     Reddit reddit = new Reddit();
            //    Reddit reddit = new RedditSharpPCL Reddit();
             //   RedditUser user = await Task.Factory.StartNew(() => { return reddit.LogIn(username, password); });
           //     MessageBox.Show("Successful login");

                // NavigationService.Navigate(new Uri("/UserPage.xaml?name=",UriKind.Relative));
                //NavigationService.Navigate(new Uri("/SubredditContent.xaml?key=" + user.FullName + "&comments=" + user.Posts + "&createdat=" + user.Created.ToString() , UriKind.Relative));



            await Task.Factory.StartNew(() =>
               {
                   Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/SubredditContent.xaml?username=" + username + "&password=" + password, UriKind.Relative)));
               }
               );
            
     

            //Subreddit s = await Task.Factory.StartNew(() => { return reddit.GetSubreddit("fatpeoplehate"); });
            //Subreddit f = await Task.Run() => reddit.GetSubreddit("windowsphone")
            //MessageBox.Show(s.Subscribers.ToString());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SideMenu2.xaml?", UriKind.Relative));
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
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
    public partial class SideMenu2 : PhoneApplicationPage
    {
        MainPage authentication = new MainPage();
        public int logCheck;

        public SideMenu2()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            logCheck = 0;

            if (NavigationContext.QueryString.ContainsKey("isloggedin"))
            {
                string key = NavigationContext.QueryString["isloggedin"];
                logCheck = Convert.ToInt32(key);
            }

            await disableButtons();
            
        }

        /// <summary>
        /// Button links to SubredditContent class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SubredditContent.xaml?subreddits=" + subredditTxt.Text, UriKind.Relative));
        }

        /// <summary>
        /// Button links to UserPage class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/UserPage.xaml?", UriKind.Relative));


        }

        /// <summary>
        /// Button links to Authentication to login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
        }

        private async Task disableButtons()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    //LoggedInReddit.LogIn(user, pass);
                    if (logCheck == 1)
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            //LogIn.Content = "Log Out";
                            LogIn.Opacity = 0;
                            LogIn.IsEnabled = false;
                            UserProfile.Opacity = 100;
                            UserProfile.IsEnabled = true;
                        }
                        );
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            LogIn.Opacity = 100;
                            LogIn.IsEnabled = true;
                            UserProfile.Opacity = 0;
                            UserProfile.IsEnabled = false;
                        }
                        );

                    }
                }
                catch(Exception exc)
                {
                    MessageBox.Show("Failed" + exc);
                }

            });
        }
    }
}

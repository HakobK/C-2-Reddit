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
    public partial class SideMenu : UserControl
    {
        MainPage authentication = new MainPage();

        public SideMenu()
        {
            InitializeComponent();
            
        }

        protected async void OnNavigatedTo(NavigationEventArgs e)
        {
            await disableButtons();

        }

        /// <summary>
        /// Button links to SubredditContent class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/SubredditContent.xaml?subreddits=" + subredditTxt.Text, UriKind.Relative));
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
                //LoggedInReddit.LogIn(user, pass);
                if (authentication.loggedIn == 1)
                {
                    Dispatcher.BeginInvoke(() =>
                        {
                            LogIn.Opacity = 0;
                            LogIn.IsEnabled = false;
                        }
                    );
                }
                else
                {
                    Dispatcher.BeginInvoke(() => 
                        {
                            LogIn.Content = "Log out";
                        }
                    );

                }

            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;

namespace RedditPhone
{
    public partial class PostContent : PhoneApplicationPage
    {

        public string postID;

        public PostContent()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.ContainsKey("postID"))
            {
                string postID = NavigationContext.QueryString["postID"];
                await Task.Factory.StartNew(() =>
                {
                    Dispatcher.BeginInvoke(() => getPostContent());
                }
                );
            }

        }

        public void getPostContent()
        {
            MessageBox.Show(postID);
        }


    }

    
}
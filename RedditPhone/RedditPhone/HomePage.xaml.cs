using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RedditSharpPCL;
using System.Threading;
using System.Threading.Tasks;

namespace RedditPhone
{
    public partial class HomePage : PhoneApplicationPage
    {

        public HomePage()
        {
            InitializeComponent();
            doStuff();


        }


        static async void doStuff()
        {
            Reddit reddit = new Reddit();
            Subreddit s = new Subreddit();
            List<Post> swag = new List<Post>();

            var phone = await Task.Factory.StartNew(() => { return reddit.GetSubreddit("fatpeoplehate"); });

            var posts = phone.Posts.Take(25);
            foreach(var post in posts)
            {
                MessageBox.Show(post.Title);
            }



        }
    }
}
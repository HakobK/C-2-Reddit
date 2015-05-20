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
using System.Collections;
using System.Windows.Media.Imaging;

namespace RedditPhone
{
    public partial class HomePage : PhoneApplicationPage
    {
        public int n = 40;
        public int m = 40;
        public int x = 25;
        public int i = 0;
        public string subredditname = "";

        public HomePage()
        {
            InitializeComponent();
            rName.FontSize = 39;
            rName.Text = "Loading....";
            HotTxt.Text = "Loading...";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if(NavigationContext.QueryString.ContainsKey("subreddits"))
            {
                string subR = NavigationContext.QueryString["subreddits"];
                subredditname = subR;
            }
            doStuff(subredditname);

        }

       public async void doStuff(string subR)
        {
           
            Reddit reddit = new Reddit();

            await Task.Factory.StartNew(() => {  reddit.LogIn("schoolc2", "school123!"); });


            var phone = await Task.Factory.StartNew(() => { return reddit.GetSubreddit(subR); });


            rName.Text = phone.Title;

            var posts = await Task.Factory.StartNew(() => { return phone.Posts.Take(9); });

            var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });


            string titles = "";
            TextBlock[] Tblock = new TextBlock[x];
            Image[] Image = new Image[x];

            await Task.Factory.StartNew(() => {
                foreach (Post post in posts)
                {
                    string yolo = post.Title;


                    Dispatcher.BeginInvoke(() =>
                    {

                        //work in progress
                        //try{
                        //Uri url2 = new Uri(post.Thumbnail.ToString());
                        
                        
                        //    Image[i].Source = new BitmapImage(url2);
                        //    Image[i].Margin = new Thickness(0, n, 0, 0);
                        //    ContentPanel.Children.Add(Image[i]);
                        //}
                        //catch(Exception)
                        //{
                        //    Uri url2 = new Uri("http://icons.iconarchive.com/icons/fatcow/farm-fresh/32/reddit-icon.png");


                        //    Image[i].Source = new BitmapImage(url2);
                        //    Image[i].Margin = new Thickness(0, n, 0, 0);
                        //    ContentPanel.Children.Add(Image[i]);
                        //}

                        var txt = new TextBlock();
                        Tblock[i] = txt;
                        txt.Text = yolo;
                        txt.Margin = new Thickness(0, n, 0, 0);
                        ContentPanel.Children.Add(txt);

                        n = n + 50;

                    });

                    i++;


                }
            });
            try
            {
                Uri url = new Uri(phone.HeaderImage);
                headerImage.Opacity = 0.45;
                headerImage.Source = new BitmapImage(url);
            }
           catch(ArgumentNullException)
            {
                MessageBox.Show("Error loading picture");
            }
            HotTxt.Text = titles;            

            //foreach(var post in posts)
            //{
            //    MessageBox.Show(post.Title);
            //}
            //var posts = phone.GetPosts().Take(25);

            //foreach(var post in posts)
            //{
            //    MessageBox.Show(post.);
            //}



        }
    }
}
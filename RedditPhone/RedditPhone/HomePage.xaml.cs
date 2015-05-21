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


            var sReddit = await Task.Factory.StartNew(() => { return reddit.GetSubreddit(subR); });


            rName.Text = sReddit.Title;

            var posts = await Task.Factory.StartNew(() => { return sReddit.Posts.Take(9); });

            var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });


            string titles = "";
            TextBlock[] Tblock = new TextBlock[x];
            Image Image = new Image();

            await Task.Factory.StartNew(() => {
                foreach (Post post in posts)
                {
                    string yolo = post.Title;


                    Dispatcher.BeginInvoke(() =>
                    {
                       // MessageBox.Show(post.Thumbnail.ToString());
                        //string thumb = "";
                        //thumb = post.Thumbnail.ToString();
                        //thumb = "https://static.onthehub.com/production/attachments/9/8bd15d60-ad3d-e311-93f6-b8ca3a5db7a1/6eaf48c8-5ef4-4e81-9336-75f35f498537.jpg";
                        //if (thumb != "self")
                        //{
                        //    Uri url3 = new Uri(thumb);
                        //    Image.Source = new BitmapImage(url3);
                        //    Image.Margin = new Thickness(0, n, 0, 0);
                        //}
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
                        txt.Margin = new Thickness(50, n, 0, 0);
                        ContentPanel.Children.Add(txt);
                       // ContentPanel.Children.Add(Image);

                        n = n + 50;

                    });

                    i++;


                }
            });
            try
            {
                Uri url = new Uri(sReddit.HeaderImage);
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
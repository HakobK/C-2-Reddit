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
using System.Windows.Media;
using System.Windows.Ink;
using System.Windows.Input;

namespace RedditPhone
{
    public partial class HomePage : PhoneApplicationPage
    {
        public int n = 25;
        public int m = 40;
        public int x = 25;
        public int i = 0;
        public string subredditname = "";
        public string thumbDefault = "http://www.reddit.com/static/self_default2.png";
        public string titles = "";
        public string thumb = "";
        public string ifNotSet = "self";
        StackPanel[] panelCollection;
        TextBlock[] Tblock;
        Image[] thumbnailImage;

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

       private void StackPanel_Tap(object sender, GestureEventArgs e)
        {
            MessageBox.Show("lol nice");
        }

       private void print(object sender, System.Windows.Input.GestureEventArgs e)
       {
           MessageBox.Show("omg");
           
       }

       public async void doStuff(string subR)
        {
            Reddit reddit = new Reddit();
            await Task.Factory.StartNew(() => {  reddit.LogIn("schoolc2", "school123!"); });
            var sReddit = await Task.Factory.StartNew(() => { return reddit.GetSubreddit(subR); });
            rName.Text = sReddit.Title;
            var posts = await Task.Factory.StartNew(() => { return sReddit.Posts.Take(11); });
            var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });

            panelCollection = new StackPanel[x];
            //StackPanel[] panelCollection = new StackPanel[x];
            Tblock = new TextBlock[x];
            thumbnailImage = new Image[x];
           

            await Task.Factory.StartNew(() => {
                foreach (Post post in posts)
                {
                    string postTitle = post.Title;


                    Dispatcher.BeginInvoke(() =>
                    {
                       // MessageBox.Show(post.Thumbnail.ToString());
                        thumb = post.Thumbnail.ToString();
                        var img = new Image();
                        img.MaxHeight = 80;
                        img.MaxWidth = 80;
                        thumbnailImage[i] = img;
                        if (thumb != ifNotSet)
                        {

                            Uri url3 = new Uri(thumb);
                            img.Source = new BitmapImage(url3);
                            img.HorizontalAlignment = HorizontalAlignment.Left;

                            //img.Margin = new Thickness(0, 0, 0, 0);
                        }

                        else
                        {
                            Uri url3 = new Uri(thumbDefault);
                            img.Source = new BitmapImage(url3);
                            img.HorizontalAlignment = HorizontalAlignment.Left;
                           // img.Margin = new Thickness(0, 0, 0, 0);
                        }
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
                        txt.Text = postTitle;
                        txt.FontSize = 14;
                        txt.HorizontalAlignment = HorizontalAlignment.Center;
                      //  txt.Margin = new Thickness(60,0,0,0);
                        

                        var panel1 = new StackPanel();
                        panel1.Tap += new EventHandler<GestureEventArgs>(print);
                        panel1.MaxHeight = 70;
                        panel1.MaxWidth = 400;
                        panel1.VerticalAlignment = VerticalAlignment.Top;
                        panel1.Margin = new Thickness(0, n, 0, 0);
                        SolidColorBrush myBrush = new SolidColorBrush(Colors.Blue);
                        panel1.Background = myBrush;
                        
                        panelCollection[i] = panel1;
                        
                        panel1.Children.Add(txt);
                        panel1.Children.Add(img);
                        ContentPanel.Children.Add(panel1);
                        //StackPanel stackpanel = new StackPanel();
                        
                        //stackpanel.Children.Add(txt);

                        //stackpanel1.Margin = new Thickness(0, 0, 0, 0);

                        //ContentPanel.Children.Add(img);
                       //ContentPanel.Children.Add(stackpanel1);


                        n = n + 90;

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
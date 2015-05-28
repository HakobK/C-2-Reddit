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
    public partial class SubredditContent : PhoneApplicationPage
    {
        public int verticalMargin = 25;
        public int objectSize = 25;
        public int objectIndex = 0;
        public string username;
        public string password;
        public string thumbDefault = "http://www.reddit.com/static/self_default2.png";
        public string thumb;
        public string ifNotSet = "self";
        public Reddit LoggedInReddit = new Reddit();
        StackPanel[] panelCollection;
        TextBlock[] tBlockCollection;
        Image[] thumbnailCollection;
        RedditUser test;

        public SubredditContent()
        {
            InitializeComponent();
            rName.FontSize = 27;
            rName.Text = "Loading...";
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.ContainsKey("subreddits"))
            {
                string subR = NavigationContext.QueryString["subreddits"];
                getContentWithSubr(subR);

            }

            else
            {
                if (NavigationContext.QueryString.ContainsKey("username"))
                {
                    string usernamePass = NavigationContext.QueryString["username"];
                    username = usernamePass;
                }

                if (NavigationContext.QueryString.ContainsKey("password"))
                {
                    string usernamePass = NavigationContext.QueryString["password"];
                    password = usernamePass;
                }
                await login(username, password);
                getContentFrontPage();

            }
        }

       private void StackPanel_Tap(object sender, GestureEventArgs e)
        {
            MessageBox.Show("lol nice");
        }

       private void print(object sender, System.Windows.Input.GestureEventArgs e)
       {
           MessageBox.Show("u heeft geklikt");           
       }

       public async void getContentWithSubr(string subR)
       {
           Reddit reddit = new Reddit();
           await Task.Factory.StartNew(() => { reddit.LogIn("schoolc2", "school123!"); });
           var sReddit = await Task.Factory.StartNew(() => { return reddit.GetSubreddit(subR); });
           var posts = await Task.Factory.StartNew(() => { return sReddit.Posts.Take(11); });
           var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });
           rName.Text = sReddit.Title;
           MessageBox.Show(reddit.User.Id.ToString());
           panelCollection = new StackPanel[objectSize];
           tBlockCollection = new TextBlock[objectSize];
           thumbnailCollection = new Image[objectSize];


           await Task.Factory.StartNew(() =>
           {
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
                       thumbnailCollection[objectIndex] = img;
                       if (thumb != ifNotSet)
                       {
                           try
                           {
                               Uri url3 = new Uri(thumb);
                               img.Source = new BitmapImage(url3);
                               img.HorizontalAlignment = HorizontalAlignment.Left;
                               //img.Margin = new Thickness(0, 0, 0, 0);
                           }

                           catch (Exception)
                           {
                               Uri url3 = new Uri(thumbDefault);
                               img.Source = new BitmapImage(url3);
                               img.HorizontalAlignment = HorizontalAlignment.Left;
                               // img.Margin = new Thickness(0, 0, 0, 0);
                           }
                       }

                       else
                       {
                           Uri url3 = new Uri(thumbDefault);
                           img.Source = new BitmapImage(url3);
                           img.HorizontalAlignment = HorizontalAlignment.Left;
                           // img.Margin = new Thickness(0, 0, 0, 0);
                       }
                       var txt = new TextBlock();
                       tBlockCollection[objectIndex] = txt;
                       txt.Text = postTitle;
                       txt.FontSize = 14;
                       txt.HorizontalAlignment = HorizontalAlignment.Center;
                       //  txt.Margin = new Thickness(60,0,0,0);

                       var panel1 = new StackPanel();
                       panel1.Tap += new EventHandler<GestureEventArgs>(print);
                       panel1.MaxHeight = 70;
                       panel1.MaxWidth = 400;
                       panel1.VerticalAlignment = VerticalAlignment.Top;
                       panel1.Margin = new Thickness(0, verticalMargin, 0, 0);
                       SolidColorBrush myBrush = new SolidColorBrush(Colors.Blue);
                       panel1.Background = myBrush;
                       panelCollection[objectIndex] = panel1;
                       panel1.Children.Add(txt);
                       panel1.Children.Add(img);
                       ContentPanel.Children.Add(panel1);

                       verticalMargin = verticalMargin + 90;

                   });
                   objectIndex++;
               }
           });
           try
           {
               Uri url = new Uri(sReddit.HeaderImage);
               headerImage.Opacity = 0.45;
               headerImage.Source = new BitmapImage(url);
           }
           catch (ArgumentNullException)
           {
               MessageBox.Show("Error loading picture");
           }

       }

       public async Task login(string user, string pass)
       {
           
           
           await Task.Factory.StartNew(() =>
           {
               //LoggedInReddit.LogIn(user, pass);

               try
               {
               
                       LoggedInReddit.LogIn(username, password); 
                   
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

       public async void getContentFrontPage()
       {

           Reddit reddit = new Reddit();
           reddit = LoggedInReddit;
          // await Task.Factory.StartNew(() => { login(user, pass); }); 
           
           var sReddit = await Task.Factory.StartNew(() => { return reddit.FrontPage; });
           var posts = await Task.Factory.StartNew(() => { return sReddit.Posts.Take(11); });
           var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });
           rName.Text = sReddit.Title;
           
           panelCollection = new StackPanel[objectSize];
           //StackPanel[] panelCollection = new StackPanel[x];
           tBlockCollection = new TextBlock[objectSize];
           thumbnailCollection = new Image[objectSize];


           await Task.Factory.StartNew(() =>
           {
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
                       thumbnailCollection[objectIndex] = img;
                       if (thumb != ifNotSet)
                       {
                           try
                           {
                               Uri url3 = new Uri(thumb);
                               img.Source = new BitmapImage(url3);
                               img.HorizontalAlignment = HorizontalAlignment.Left;
                               //img.Margin = new Thickness(0, 0, 0, 0);
                           }

                           catch (Exception)
                           {
                               Uri url3 = new Uri(thumbDefault);
                               img.Source = new BitmapImage(url3);
                               img.HorizontalAlignment = HorizontalAlignment.Left;
                               // img.Margin = new Thickness(0, 0, 0, 0);
                           }
                       }

                       else
                       {
                           Uri url3 = new Uri(thumbDefault);
                           img.Source = new BitmapImage(url3);
                           img.HorizontalAlignment = HorizontalAlignment.Left;
                           // img.Margin = new Thickness(0, 0, 0, 0);
                       }
                       var txt = new TextBlock();
                       tBlockCollection[objectIndex] = txt;
                       txt.Text = postTitle;
                       txt.FontSize = 14;
                       txt.HorizontalAlignment = HorizontalAlignment.Center;
                       //  txt.Margin = new Thickness(60,0,0,0);

                       var panel1 = new StackPanel();
                       panel1.Tap += new EventHandler<GestureEventArgs>(print);
                       panel1.MaxHeight = 70;
                       panel1.MaxWidth = 400;
                       panel1.VerticalAlignment = VerticalAlignment.Top;
                       panel1.Margin = new Thickness(0, verticalMargin, 0, 0);
                       SolidColorBrush myBrush = new SolidColorBrush(Colors.Blue);
                       panel1.Background = myBrush;

                       panelCollection[objectIndex] = panel1;

                       panel1.Children.Add(txt);
                       panel1.Children.Add(img);
                       ContentPanel.Children.Add(panel1);

                       verticalMargin = verticalMargin + 90;

                   });
                   objectIndex++;
               }
           });
           try
           {
               Uri url = new Uri(sReddit.HeaderImage);
               headerImage.Opacity = 0.45;
               headerImage.Source = new BitmapImage(url);
           }
           catch (ArgumentNullException)
           {
               

           }

       }



       public async void getContentFrontPageNew()
       {
           MessageBox.Show("Getting new posts");
           ContentPanel.Children.Clear();
           panelCollection = null;
           tBlockCollection = null;
           thumbnailCollection = null;
           Reddit reddit = new Reddit();
           reddit = LoggedInReddit;
           var sReddit = await Task.Factory.StartNew(() => { return reddit.FrontPage; });
           var posts = await Task.Factory.StartNew(() => { return sReddit.New.Take(11); });
           var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });
           rName.Text = sReddit.Title;
           Post test = new Post();
          
           panelCollection = new StackPanel[objectSize];
           //StackPanel[] panelCollection = new StackPanel[x];
           tBlockCollection = new TextBlock[objectSize];
           thumbnailCollection = new Image[objectSize];


           await Task.Factory.StartNew(() =>
           {
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
                       thumbnailCollection[objectIndex] = img;
                       if (thumb != ifNotSet)
                       {
                           try
                           {
                               Uri url3 = new Uri(thumb);
                               img.Source = new BitmapImage(url3);
                               img.HorizontalAlignment = HorizontalAlignment.Left;
                               //img.Margin = new Thickness(0, 0, 0, 0);
                           }

                           catch (Exception)
                           {
                               Uri url3 = new Uri(thumbDefault);
                               img.Source = new BitmapImage(url3);
                               img.HorizontalAlignment = HorizontalAlignment.Left;
                               // img.Margin = new Thickness(0, 0, 0, 0);
                           }
                       }

                       else
                       {
                           Uri url3 = new Uri(thumbDefault);
                           img.Source = new BitmapImage(url3);
                           img.HorizontalAlignment = HorizontalAlignment.Left;
                           // img.Margin = new Thickness(0, 0, 0, 0);
                       }
                       var txt = new TextBlock();
                       tBlockCollection[objectIndex] = txt;
                       txt.Text = postTitle;
                       txt.FontSize = 14;
                       txt.HorizontalAlignment = HorizontalAlignment.Center;
                       //  txt.Margin = new Thickness(60,0,0,0);

                       var panel1 = new StackPanel();
                       panel1.Tap += new EventHandler<GestureEventArgs>(print);
                       panel1.MaxHeight = 70;
                       panel1.MaxWidth = 400;
                       panel1.VerticalAlignment = VerticalAlignment.Top;
                       panel1.Margin = new Thickness(0, verticalMargin, 0, 0);
                       SolidColorBrush myBrush = new SolidColorBrush(Colors.Blue);
                       panel1.Background = myBrush;

                       panelCollection[objectIndex] = panel1;

                       panel1.Children.Add(txt);
                       panel1.Children.Add(img);
                       ContentPanel.Children.Add(panel1);

                       verticalMargin = verticalMargin + 90;

                   });
                   objectIndex++;
               }
           });
           try
           {
               Uri url = new Uri(sReddit.HeaderImage);
               headerImage.Opacity = 0.45;
               headerImage.Source = new BitmapImage(url);
           }
           catch (ArgumentNullException)
           {


           }

       }


       private void newTap(object sender, GestureEventArgs e)
       {
           getContentFrontPageNew();
       }
    }
}
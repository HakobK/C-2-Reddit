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
        MainPage authentication = new MainPage();

        public int verticalMargin = 25;
        public int objectSize = 25;
        public int objectIndex = 0;
        public string username;
        public string password;
        public string thumbDefault = "http://www.reddit.com/static/self_default2.png";
        public string thumb;
        public string ifNotSet = "self";
        public Reddit LoggedInReddit = new Reddit();
        public Grid[] gridCollection;
        public TextBlock[] tBlockCollection;
        public Image[] thumbnailCollection;
        public Comment[] postListComments;
        public string subredditStatus;
        public int isLoggedIn;

        public SubredditContent()
        {
            InitializeComponent();

            gridCollection = new Grid[objectSize];
            tBlockCollection = new TextBlock[objectSize];
            thumbnailCollection = new Image[objectSize];

            rName.FontSize = 27;
            rName.TextWrapping = TextWrapping.Wrap;
            rName.Text = "Loading...";
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.ContainsKey("subreddits"))
            {
                string subR = NavigationContext.QueryString["subreddits"];
                subredditStatus = subR;
                getContentWithSubr(subR);
            }

            else
            {
                if (NavigationContext.QueryString.ContainsKey("loggedin"))
                {
                    subredditStatus = "frontpageloggedin";
                    isLoggedIn = 1;
                    await getContentFrontPage();
                }
            }
        }

        private async void print(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show("u heeft geklikt");
            Post test22 = new Post();

            await Task.Factory.StartNew(() =>
            {
                Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/PostContent.xaml?postID=" + test22.Url, UriKind.Relative)));
            }
                );
        }

       public async void getContentWithSubr(string subR)
       {
           Reddit reddit = new Reddit();
           var sReddit = await Task.Factory.StartNew(() => { return reddit.GetSubreddit(subR); });
           IEnumerable<Post> posts = await Task.Factory.StartNew(() => { return sReddit.Posts.Take(11); });
           var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });
           rName.Text = sReddit.Title;
           fillPageWithPosts(posts);
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

       public async Task getContentFrontPage()
       {

           Reddit reddit = new Reddit();
           reddit = authentication.authenticatedReddit;
          // await Task.Factory.StartNew(() => { login(user, pass); }); 
           
           var sReddit = await Task.Factory.StartNew(() => { return reddit.FrontPage; });
           var posts = await Task.Factory.StartNew(() => { return sReddit.Posts.Take(11); });
           var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });
           rName.Text = sReddit.Title;
           fillPageWithPosts(posts);

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
           tBlockCollection = null;
           thumbnailCollection = null;
           verticalMargin = 25;
           objectSize = 25;
           objectIndex = 0;
           Reddit reddit = new Reddit();
           reddit = LoggedInReddit;
           var sReddit = await Task.Factory.StartNew(() => { return reddit.FrontPage; });
           var posts = await Task.Factory.StartNew(() => { return sReddit.New.Take(11); });
           var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });
           rName.Text = sReddit.Title;

           fillPageWithPosts(posts);

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

        public async void fillPageWithPosts(IEnumerable<Post> posts)
       {
           await Task.Factory.StartNew(() =>
           {
               foreach (Post post in posts)
               {
                   string postTitle = post.Title;
                   Dispatcher.BeginInvoke(() =>
                   {
                       thumb = post.Thumbnail.ToString();
                       var img = new Image();
                       img.Height = 80;
                       img.Width = 80;
                       img.Margin = new Thickness(10, 0, 0, 0);
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
                               img.VerticalAlignment = VerticalAlignment.Bottom;
                               img.MaxHeight = 70;
                               img.MaxWidth = 100;
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
                       TextBlock txt = new TextBlock();
                       tBlockCollection[objectIndex] = txt;
                       txt.Text = postTitle;
                       txt.FontSize = 14;

                       txt.Margin = new Thickness(95, 0, 0, 0);
                       txt.TextWrapping = TextWrapping.Wrap;
                       //  txt.Margin = new Thickness(60,0,0,0);

                       var panel1 = new Grid();
                       //Dispatcher.BeginInvoke(() => { postListComments = post.Comments; });
                       panel1.Tap += new EventHandler<GestureEventArgs>(print);
                       panel1.MaxHeight = 70;
                       panel1.Height = 80;
                       panel1.Width = 465;
                       panel1.MaxWidth = 465;
                       panel1.VerticalAlignment = VerticalAlignment.Top;
                       panel1.Margin = new Thickness(0, verticalMargin, 0, 0);
                       SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 35, 35, 35));
                       panel1.Background = myBrush;

                       gridCollection[objectIndex] = panel1;
                       panel1.Children.Add(txt);
                       panel1.Children.Add(img);

                       ContentPanel.Children.Add(panel1);

                       verticalMargin = verticalMargin + 90;

                   });

                   objectIndex++;

               }
           });
       }



       private void newTap(object sender, GestureEventArgs e)
       {
           getContentFrontPageNew();
       }

       private void Button_Click(object sender, RoutedEventArgs e)
       {
           NavigationService.Navigate(new Uri("/SideMenu2.xaml?isloggedin=" + isLoggedIn, UriKind.Relative));
       }

       private void newTxt_Tap(object sender, GestureEventArgs e)
       {
           getContentFrontPageNew();
       }

       private void StackPanel_Tap(object sender, GestureEventArgs e)
       {
           
       }
    }
}

//await Task.Factory.StartNew(() =>
//{
//    foreach (Post post in posts)
//    {

//        string postTitle = post.Title;
//        Dispatcher.BeginInvoke(() =>
//        {
//            // MessageBox.Show(post.Thumbnail.ToString());
//            thumb = post.Thumbnail.ToString();
//            var img = new Image();
//            img.Height = 80;
//            img.Width = 80;
//            img.Margin = new Thickness(10, 0, 0, 0);
//            thumbnailCollection[objectIndex] = img;

//            if (thumb != ifNotSet)
//            {
//                try
//                {
//                    Uri url3 = new Uri(thumb);
//                    img.Source = new BitmapImage(url3);
//                    img.HorizontalAlignment = HorizontalAlignment.Left;
//                    //img.Margin = new Thickness(0, 0, 0, 0);
//                }

//                catch (Exception)
//                {
//                    Uri url3 = new Uri(thumbDefault);
//                    img.Source = new BitmapImage(url3);
//                    img.HorizontalAlignment = HorizontalAlignment.Left;
//                    img.VerticalAlignment = VerticalAlignment.Bottom;
//                    img.MaxHeight = 70;
//                    img.MaxWidth = 100;
//                    // img.Margin = new Thickness(0, 0, 0, 0);
//                }
//            }

//            else
//            {
//                Uri url3 = new Uri(thumbDefault);
//                img.Source = new BitmapImage(url3);
//                img.HorizontalAlignment = HorizontalAlignment.Left;
//                // img.Margin = new Thickness(0, 0, 0, 0);
//            }
//            TextBlock txt = new TextBlock();
//            tBlockCollection[objectIndex] = txt;
//            txt.Text = postTitle;
//            txt.FontSize = 14;

//            txt.Margin = new Thickness(95, 0, 0, 0);
//            txt.TextWrapping = TextWrapping.Wrap;
//            //  txt.Margin = new Thickness(60,0,0,0);

//            var panel1 = new Grid();
//            //Dispatcher.BeginInvoke(() => { postListComments = post.Comments; });
//            panel1.Tap += new EventHandler<GestureEventArgs>(print);
//            panel1.MaxHeight = 70;
//            panel1.Height = 80;
//            panel1.Width = 465;
//            panel1.MaxWidth = 465;
//            panel1.VerticalAlignment = VerticalAlignment.Top;
//            panel1.Margin = new Thickness(0, verticalMargin, 0, 0);
//            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 35, 35, 35));
//            panel1.Background = myBrush;

//            gridCollection[objectIndex] = panel1;
//            panel1.Children.Add(txt);
//            panel1.Children.Add(img);

//            ContentPanel.Children.Add(panel1);

//            verticalMargin = verticalMargin + 90;

//        });

//        objectIndex++;

//    }
//});

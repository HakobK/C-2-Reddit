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


       public async void doStuff()
        {
           
            Reddit reddit = new Reddit();

            await Task.Factory.StartNew(() => {  reddit.LogIn("schoolc2", "school123!"); });

            var phone = await Task.Factory.StartNew(() => { return reddit.GetSubreddit("fatpeoplehate"); });

            rName.Text = phone.Title;
            HotTxt.Text = phone.PublicDescription;

            var posts = await Task.Factory.StartNew(() => { return phone.Posts.Take(5); });
            MessageBox.Show("1");

            var text = await Task.Factory.StartNew(() => { return posts.Count().ToString(); });

            MessageBox.Show(text);

            string titles = "";

            await Task.Factory.StartNew(() => {
                foreach (Post post in posts)
                {
                    string yolo = post.Title;
                    TextBlock block = new TextBlock();
                    block.Text = yolo;
                    ContentPanel.Children.Add(block);


                    
                }
            });



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
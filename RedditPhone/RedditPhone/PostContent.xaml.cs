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
using System.Threading.Tasks;

namespace RedditPhone
{
    public partial class PostContent : PhoneApplicationPage
    {
        SubredditContent subredditPage = new SubredditContent();
        public Comment[] comments;
 

        public PostContent()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            comments = subredditPage.postListComments;
            if(comments == null)
            {
                MessageBox.Show("list is empty");
            }
            else
            {
                MessageBox.Show("List is not empty");
                await Task.Factory.StartNew(() =>
                {
                    Dispatcher.BeginInvoke(() =>
                    { foreach (Comment com in comments) { MessageBox.Show(com.Body); } }
                    );

                });
            }
            

            

            //if (NavigationContext.QueryString.ContainsKey("postID"))
            //{
            //    postID = NavigationContext.QueryString["postID"];
            //    await Task.Factory.StartNew(() =>
            //    {
            //        Dispatcher.BeginInvoke(() => fillPagewithPost(post));
            //    }
            //    );
            //}

        }

        //public void fillPagewithPost(Post post)
        //{
        //    Post newPost = new Post();
        //    post.Id = postID;
        //    //textBoxPost.Text = newPost.Id;
        //    textBoxPost.Text = post.Title;
        //    Reddit s = new Reddit();
        //    Post g = new Post();
        //    //MessageBox.Show("hallo" + postID);
        //}

        public void submitComment(Post post, string message)
        {

        }

        public void submitCommentComment(Post post, Comment comment, string message)
        {

        }

        public void voteComment(Post post, Comment comment, string upOrDown)
        {

        }
    }
}
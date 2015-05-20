using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditSharpPCL;

namespace RedditPhone
{
    public class Subreddit
    {
        public Subreddit()
        {

        }

        public string Name { set; get; }
        public int ActiveUsers { set; get; }
        public List<Comment> Comments { set; get; }
        public DateTime Created { set; get; }
        public string Description { set; get; }
        public string DisplayName { set; get; }
        public string HeaderImage { set; get; }
        public string HeaderTitel { set; get; }
        public List<Post> Hot { set; get; }
        public List<Post> New { set; get; }
        public bool NSFW { set; get; }
        public List<Post> Posts { set; get; }
        public string PublicDescription { set; get; }
        public int Subscribers { set; get; }
        public string Title { set; get; }
        public Uri Uri { set; get; }



    }
}

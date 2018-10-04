using RedditAPI = Reddit.NET;
using Reddit.NET.Controllers;
using System;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: Example.exe <Reddit Access Token>");
            }
            else
            {
                string accessToken = args[0];

                RedditAPI.Reddit reddit = new RedditAPI.Reddit(accessToken);

                User me = reddit.User().Me();

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                var blah = reddit.Models.Account.Prefs("friends");
                int i = 0;
            }
        }
    }
}

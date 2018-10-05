using Reddit.NET;
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
                Console.WriteLine("Usage: Example.exe <Reddit Refresh Token> [Reddit Access Token]");
            }
            else
            {
                string refreshToken = args[0];
                string accessToken = (args.Length > 1 ? args[1] : null);

                RedditAPI reddit = new RedditAPI(refreshToken, accessToken);

                User me = reddit.User().Me();

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                var blah = reddit.Models.Emoji.All("WayOfTheBern");
                int i = 0;
            }
        }
    }
}

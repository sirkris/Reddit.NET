using Reddit.NET;
using Reddit.NET.Controllers;
using System;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Example.exe <Reddit App ID> <Reddit Refresh Token> [Reddit Access Token]");
            }
            else
            {
                string appId = args[0];
                string refreshToken = args[1];
                string accessToken = (args.Length > 2 ? args[2] : null);

                RedditAPI reddit = new RedditAPI(appId, refreshToken, accessToken);

                User me = reddit.User().Me();

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                var blah = reddit.Models.Emoji.All("WayOfTheBern");
                int i = 0;
            }
        }
    }
}

using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

            Console.WriteLine("Username: " + reddit.Account.Me.Name);

            Console.WriteLine("Cake Day: " + reddit.Account.Me.Created.ToString("D"));
        }
    }
}

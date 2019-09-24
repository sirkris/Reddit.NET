using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Reddit;
using Reddit.Android;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using Newtonsoft.Json;

namespace Example.Mobile.Droid
{
    [Activity(Label = "Example.Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public MainPage MainPage { get; private set; }
        public RedditAPI Reddit { get; private set; }
        public Subreddit Subreddit { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Reddit = new RedditAPI(appId: "g19SDjZfOCq6mg", refreshToken: System.Environment.GetEnvironmentVariable("refreshToken"));
            Subreddit = Reddit.Subreddit("AskReddit");

            MainPage = new MainPage(Reddit, Subreddit);
            MainPage.ButtonClicked += C_ButtonClicked;

            LoadApplication(new App(MainPage));
        }

        private List<string> SimplifyPosts(List<Post> posts)
        {
            List<string> res = new List<string>();
            foreach (Post post in posts)
            {
                res.Add(post.Fullname);
            }

            return res;
        }

        public void C_ButtonClicked(object sender, System.EventArgs e)
        {
            if (MainPage.Active)
            {
                List<Post> posts = new List<Post>();
                posts = Subreddit.Posts.GetNew();
                List<string> simplePosts = SimplifyPosts(posts);
                string lastRes = JsonConvert.SerializeObject(simplePosts);
                Subreddit.Posts.MonitorNewAndroid(lastRes: lastRes);
                Events.NewPostsUpdated += C_NewPostsReceived;

                Toast.MakeText(Application.Context, "Monitoring Started....", ToastLength.Short).Show();

                while (MainPage.Active)
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }
            else
            {
                Events.NewPostsUpdated -= C_NewPostsReceived;
                Subreddit.Posts.MonitorNewAndroid();

                Toast.MakeText(Application.Context, "Monitoring Complete.", ToastLength.Long).Show();
            }
        }

        public void C_NewPostsReceived(object sender, PostsUpdateEventArgs e)
        {
            Toast.MakeText(Application.Context, "Post(s) received: " + e.Added.Count.ToString(), ToastLength.Short).Show();
        }
    }
}

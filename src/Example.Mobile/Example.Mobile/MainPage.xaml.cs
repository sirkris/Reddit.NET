using Reddit;
using Reddit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Example.Mobile
{
    public partial class MainPage : ContentPage
    {
        public event EventHandler<System.EventArgs> ButtonClicked;
        public bool Active { get; private set; }
        public RedditAPI Reddit { get; private set; }
        public Subreddit Subreddit { get; private set; }

        public MainPage(RedditAPI reddit, Subreddit subreddit)
        {
            InitializeComponent();
            Reddit = reddit;
            Subreddit = subreddit;
            Active = false;
        }

        private void StartButton_Pressed(object sender, System.EventArgs e)
        {
            UpdateButton();
            ButtonClicked?.Invoke(this, e);
        }

        private void UpdateButton()
        {
            if (!Active)
            {
                StartButton.Text = "Stop";
                StartButton.TextColor = Color.Yellow;
                StartButton.BackgroundColor = Color.Red;
            }
            else
            {
                StartButton.Text = "Start";
                StartButton.TextColor = Color.LawnGreen;
                StartButton.BackgroundColor = Color.DarkGreen;
            }

            Active = !Active;
        }
    }
}

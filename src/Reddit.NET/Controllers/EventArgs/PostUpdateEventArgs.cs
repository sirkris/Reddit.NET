namespace Reddit.Controllers.EventArgs
{
    public class PostUpdateEventArgs
    {
        public Post OldPost { get; set; }
        public Post NewPost { get; set; }
    }
}

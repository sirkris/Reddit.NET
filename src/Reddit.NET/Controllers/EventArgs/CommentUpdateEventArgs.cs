namespace Reddit.Controllers.EventArgs
{
    public class CommentUpdateEventArgs
    {
        public Comment OldComment { get; set; }
        public Comment NewComment { get; set; }
    }
}

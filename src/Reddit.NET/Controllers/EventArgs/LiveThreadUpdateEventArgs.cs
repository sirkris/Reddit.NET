
namespace Reddit.NET.Controllers.EventArgs
{
    public class LiveThreadUpdateEventArgs
    {
        public LiveThread OldThread { get; set; }
        public LiveThread NewThread { get; set; }
    }
}

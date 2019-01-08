using Reddit.Things;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class MessagesUpdateEventArgs
    {
        public List<Message> OldMessages { get; set; }
        public List<Message> NewMessages { get; set; }
        public List<Message> Added { get; set; }
        public List<Message> Removed { get; set; }
    }
}

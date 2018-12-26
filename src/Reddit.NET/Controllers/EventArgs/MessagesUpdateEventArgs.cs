using Reddit.Things;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class MessagesUpdateEventArgs
    {
        public List<Things.Message> OldMessages { get; set; }
        public List<Things.Message> NewMessages { get; set; }
        public List<Things.Message> Added { get; set; }
        public List<Things.Message> Removed { get; set; }
    }
}

using Reddit.Things;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class LiveThreadContributorsUpdateEventArgs
    {
        public List<Things.UserListContainer> OldContributors { get; set; }
        public List<Things.UserListContainer> NewContributors { get; set; }
        public List<Things.UserListContainer> Added { get; set; }
        public List<Things.UserListContainer> Removed { get; set; }
    }
}

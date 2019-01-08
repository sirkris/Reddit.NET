using Reddit.Things;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class LiveThreadContributorsUpdateEventArgs
    {
        public List<UserListContainer> OldContributors { get; set; }
        public List<UserListContainer> NewContributors { get; set; }
        public List<UserListContainer> Added { get; set; }
        public List<UserListContainer> Removed { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class MoreChildren
    {
        public List<Comment> Comments { get; set; }
        public List<MoreData> MoreData { get; set; }

        public MoreChildren(List<Comment> comments, List<MoreData> moreData)
        {
            Comments = comments;
            MoreData = moreData;
        }

        public MoreChildren()
        {
            Comments = new List<Comment>();
            MoreData = new List<MoreData>();
        }
    }
}

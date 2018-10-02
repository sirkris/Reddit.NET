using Reddit.NET.Controllers;
using System;

namespace Reddit.NET
{
    public class Reddit
    {
        public Dispatch Dispatch
        {
            get;
            private set;
        }

        public Reddit(string accessToken)
        {
            this.Dispatch = new Dispatch(accessToken);
        }
    }
}

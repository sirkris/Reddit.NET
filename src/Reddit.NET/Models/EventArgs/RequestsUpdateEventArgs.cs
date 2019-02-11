using System;
using System.Collections.Generic;

namespace Reddit.Models.EventArgs
{
    public class RequestsUpdateEventArgs
    {
        public List<DateTime> Requests { get; set; }
    }
}

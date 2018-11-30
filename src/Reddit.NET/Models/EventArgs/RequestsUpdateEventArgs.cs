using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.EventArgs
{
    public class RequestsUpdateEventArgs
    {
        public List<DateTime> Requests { get; set; }
    }
}

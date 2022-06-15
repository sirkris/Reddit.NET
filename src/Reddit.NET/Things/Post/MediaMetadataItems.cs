using System;
using System.Collections.Generic;

namespace Reddit.NET.Things.Post
{
    [Serializable]
    public class MediaMetadataItem
    {
            public string status { get; set; }
            public string e { get; set; }
            public string m { get; set; }
            public List<O> o { get; set; }
            public List<P> p { get; set; }
            public S s { get; set; }
            public string id { get; set; }
    }

    [Serializable]
    public class O
    {
        public int y { get; set; }
        public int x { get; set; }
        public string u { get; set; }
    }

    [Serializable]
    public class P
    {
        public int y { get; set; }
        public int x { get; set; }
        public string u { get; set; }
    }

    [Serializable]
    public class S
    {
        public int y { get; set; }
        public int x { get; set; }
        public string u { get; set; }
    }
}

using System;
using System.Xml.Serialization;

namespace Reddit.Things
{
    [Serializable]
    [XmlRoot("PostResponse", IsNullable = false)]
    public class S3PostResponse
    {
        [XmlElement("Location")]
        public string Location { get; set; }

        [XmlElement("Bucket")]
        public string Bucket { get; set; }

        [XmlElement("Key")]
        public string Key { get; set; }
        
        [XmlElement("ETag")]
        public string ETag { get; set; }
    }
}

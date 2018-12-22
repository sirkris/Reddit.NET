using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.NET.Exceptions;
using Reddit.NET.Models.EventArgs;
using Reddit.NET.Models.Internal;
using RestSharp;
using ControlStructures = Reddit.NET.Controllers.Structures;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Reddit.NET.Models
{
    public abstract class BaseModel : Request
    {
        internal override string AppId { get; set; }
        internal override string AccessToken { get; set; }
        internal override string RefreshToken { get; set; }
        internal override string DeviceId { get; set; }

        internal override List<DateTime> Requests { get; set; }

        public BaseModel(string appId, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, refreshToken, accessToken, ref restClient, deviceId)
        {
            Requests = new List<DateTime>();
        }

        public string Sr(string subreddit)
        {
            return (!string.IsNullOrWhiteSpace(subreddit) ? "/r/" + subreddit + "/" : "");
        }
    }
}

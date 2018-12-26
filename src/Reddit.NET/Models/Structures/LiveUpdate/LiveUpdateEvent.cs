using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LiveUpdateEvent
    {
        [JsonProperty("total_views")]
        public int? TotalViews;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("description_html")]
        public string DescriptionHTML;

        [JsonProperty("created")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Created;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime CreatedUTC;

        [JsonProperty("button_cta")]
        public string ButtonCTA;

        [JsonProperty("websocket_url")]
        public string WebsocketURL;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("is_announcement")]
        public bool IsAnnouncement;

        [JsonProperty("state")]
        public string State;

        [JsonProperty("announcement_url")]
        public string AnnouncementURL;

        [JsonProperty("nsfw")]
        public bool NSFW;

        [JsonProperty("viewer_count")]
        public int ViewerCount;

        [JsonProperty("num_times_dismissable")]
        public int NumTimesDismissable;

        [JsonProperty("viewer_count_fuzzed")]
        public bool ViewerCountFuzzed;

        [JsonProperty("resources_html")]
        public string ResourcesHTML;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("resources")]
        public string Resources;

        [JsonProperty("icon")]
        public string Icon;

        public LiveUpdateEvent(Controllers.LiveThread liveThread)
        {
            Id = liveThread.Id;
            Description = liveThread.Description;
            NSFW = liveThread.NSFW;
            Resources = liveThread.Resources;
            Title = liveThread.Title;
            TotalViews = liveThread.TotalViews;
            Created = liveThread.Created;
            Name = liveThread.Fullname;
            WebsocketURL = liveThread.WebsocketURL;
            AnnouncementURL = liveThread.AnnouncementURL;
            State = liveThread.State;
            ViewerCount = liveThread.ViewerCount;
            Icon = liveThread.Icon;
        }

        public LiveUpdateEvent() { }
    }
}

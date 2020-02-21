using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveUpdateEvent
    {
        [JsonProperty("total_views")]
        public int? TotalViews { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHTML { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("button_cta")]
        public string ButtonCTA { get; set; }

        [JsonProperty("websocket_url")]
        public string WebsocketURL { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_announcement")]
        public bool IsAnnouncement { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("announcement_url")]
        public string AnnouncementURL { get; set; }

        [JsonProperty("nsfw")]
        public bool NSFW { get; set; }

        [JsonProperty("viewer_count")]
        public int ViewerCount { get; set; }

        [JsonProperty("num_times_dismissable")]
        public int NumTimesDismissable { get; set; }

        [JsonProperty("viewer_count_fuzzed")]
        public bool ViewerCountFuzzed { get; set; }

        [JsonProperty("resources_html")]
        public string ResourcesHTML { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resources")]
        public string Resources { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        public LiveUpdateEvent(Controllers.LiveThread liveThread)
        {
            Id = liveThread.Id;
            Description = liveThread.Description;
            NSFW = liveThread.NSFW;
            Resources = liveThread.Resources;
            Title = liveThread.Title;
            TotalViews = liveThread.TotalViews;
            CreatedUTC = (liveThread.Created ?? default(DateTime));
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

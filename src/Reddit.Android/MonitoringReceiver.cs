using Android.Content;
using Newtonsoft.Json;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using Reddit.Exceptions;
using Message = Reddit.Things.Message;
using System.Collections.Generic;

namespace Reddit.Android
{
    [BroadcastReceiver]
    public class MonitoringReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string key = intent.GetStringExtra("key");
            string subKey = intent.GetStringExtra("subKey");
            int monitoringDelayMs = intent.GetIntExtra("monitoringDelayMs", 15000);

            string lastRes = intent.GetStringExtra("lastRes");

            RedditAPI reddit = new RedditAPI(appId: intent.GetStringExtra("appId"), appSecret: intent.GetStringExtra("appSecret"),
                accessToken: intent.GetStringExtra("accessToken"), refreshToken: intent.GetStringExtra("refreshToken"));
            switch (key.Trim().ToLower())
            {
                default:
                    throw new RedditMonitoringException("Unrecognized key '" + key + "'!");
                case "PrivateMessagesInbox":
                {
                    List<Message> newList = reddit.Account.Messages.GetMessagesInbox();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Message>>(lastRes) : new List<Message>()), newList, "inbox");

                    // Schedule the next intent.  --Kris
                    reddit.Account.Messages.MonitorInboxAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "PrivateMessagesSent":
                {
                    List<Message> newList = reddit.Account.Messages.GetMessagesSent();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Message>>(lastRes) : new List<Message>()), newList, "sent");

                    // Schedule the next intent.  --Kris
                    reddit.Account.Messages.MonitorSentAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "PrivateMessagesUnread":
                {
                    List<Message> newList = reddit.Account.Messages.GetMessagesUnread();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Message>>(lastRes) : new List<Message>()), newList, "unread");

                    // Schedule the next intent.  --Kris
                    reddit.Account.Messages.MonitorUnreadAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "BestPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetBest();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "best", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorBestAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "HotPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetHot();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "hot", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorHotAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "NewPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetNew();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "new", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorNewAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "RisingPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetRising();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "rising", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorRisingAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "TopPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetTop();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "top", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorTopAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "ControversialPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetControversial();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "controversial", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorControversialAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "ModQueuePosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetModQueue();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "modqueue", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorModQueueAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "ModQueueReportsPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetModQueueReports();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "modqueuereports", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorModQueueReportsAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "ModQueueSpamPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetModQueueSpam();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "modqueuespam", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorModQueueSpamAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "ModQueueUnmoderatedPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetModQueueUnmoderated();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "modqueueunmoderated", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorModQueueUnmoderatedAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
                case "ModQueueEditedPosts":
                {
                    List<Post> newList = reddit.Subreddit(subKey).Posts.GetModQueueEdited();
                    ListCompare(reddit, (!string.IsNullOrWhiteSpace(lastRes) ? JsonConvert.DeserializeObject<List<Post>>(lastRes) : new List<Post>()), newList, "modqueueedited", subKey);

                    // Schedule the next intent.  --Kris
                    reddit.Subreddit(subKey).Posts.MonitorModQueueEditedAndroid(monitoringDelayMs, JsonConvert.SerializeObject(newList));

                    break;
                }
            }
        }

        private void ListCompare(RedditAPI reddit, List<Message> oldList, List<Message> newList, string type)
        {
            if (reddit.Account.Messages.Lists.ListDiff(oldList, newList, out List<Message> added, out List<Message> removed))
            {
                // Event handler to alert the calling app that the list has changed.  --Kris
                MessagesUpdateEventArgs args = new MessagesUpdateEventArgs
                {
                    NewMessages = newList,
                    OldMessages = oldList,
                    Added = added,
                    Removed = removed
                };
                reddit.Account.Messages.TriggerUpdate(args, type);
            }
        }

        private void ListCompare(RedditAPI reddit, List<Post> oldList, List<Post> newList, string type, string subreddit)
        {
            if (reddit.Account.Messages.Lists.ListDiff(oldList, newList, out List<Post> added, out List<Post> removed))
            {
                // Event handler to alert the calling app that the list has changed.  --Kris
                PostsUpdateEventArgs args = new PostsUpdateEventArgs
                {
                    NewPosts = newList,
                    OldPosts = oldList,
                    Added = added,
                    Removed = removed
                };
                reddit.Subreddit(subreddit).Posts.TriggerUpdate(args, type);
            }
        }
    }
}

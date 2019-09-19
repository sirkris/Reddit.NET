using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Reddit.Controllers.EventArgs;
using Reddit.Exceptions;
using Reddit.Things;
using Message = Reddit.Things.Message;

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

            RedditAPI reddit = new RedditAPI(appId: intent.GetStringExtra("appId"), appSecret: intent.GetStringExtra("appSecret"),
                accessToken: intent.GetStringExtra("accessToken"), refreshToken: intent.GetStringExtra("refreshToken"));
            switch (key.Trim().ToLower())
            {
                default:
                    throw new RedditMonitoringException("Unrecognized key '" + key + "'!");
                case "PrivateMessagesInbox":
                    List<Message> oldList = reddit.Account.Messages.inbox;
                    List<Message> newList = reddit.Account.Messages.GetMessagesInbox();

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
                        reddit.Account.Messages.TriggerUpdate(args, "inbox");
                    }

                    // Schedule the next intent.  --Kris
                    reddit.Account.Messages.MonitorInboxAndroid(monitoringDelayMs);

                    break;
            }
        }
    }
}

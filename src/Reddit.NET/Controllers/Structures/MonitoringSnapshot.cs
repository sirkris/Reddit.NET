using Reddit.Exceptions;
using System.Collections.Generic;

namespace Reddit.Controllers.Structures
{
    public class MonitoringSnapshot
    {
        public List<string> BestPosts;
        public List<string> HotPosts;
        public List<string> NewPosts;
        public List<string> RisingPosts;
        public List<string> TopPosts;
        public List<string> ControversialPosts;
        public List<string> ModQueuePosts;
        public List<string> ModQueueReportsPosts;
        public List<string> ModQueueSpamPosts;
        public List<string> ModQueueUnmoderatedPosts;
        public List<string> ModQueueEditedPosts;
        public List<string> PrivateMessagesInbox;
        public List<string> PrivateMessagesUnread;
        public List<string> PrivateMessagesSent;
        public List<string> ConfidenceComments;
        public List<string> TopComments;
        public List<string> NewComments;
        public List<string> ControversialComments;
        public List<string> OldComments;
        public List<string> RandomComments;
        public List<string> QAComments;
        public List<string> LiveComments;
        public List<string> WikiPages;
        public List<string> WikiPage;
        public List<string> ModmailMessagesRecent;
        public List<string> ModmailMessagesMod;
        public List<string> ModmailMessagesUser;
        public List<string> ModmailMessagesUnread;
        public List<string> LiveThread;
        public List<string> LiveThreadContributors;
        public List<string> LiveThreadUpdates;
        public List<string> PostData;
        public List<string> PostScore;
        public List<string> PostHistory;
        public List<string> CommentHistory;

        public MonitoringSnapshot(List<string> bestPosts = null, List<string> hotPosts = null, List<string> newPosts = null, List<string> risingPosts = null, 
            List<string> topPosts = null, List<string> controversialPosts = null, List<string> modQueuePosts = null, List<string> modQueueReportsPosts = null, 
            List<string> modQueueSpamPosts = null, List<string> modQueueUnmoderatedPosts = null, List<string> modQueueEditedPosts = null, 
            List<string> privateMessagesInbox = null, List<string> privateMessagesUnread = null, List<string> privateMessagesSent = null, 
            List<string> confidenceComments = null, List<string> topComments = null, List<string> newComments = null, List<string> controversialComments = null, 
            List<string> oldComments = null, List<string> randomComments = null, List<string> qaComments = null, List<string> liveComments = null, 
            List<string> wikiPages = null, List<string> wikiPage = null, List<string> modmailMessagesRecent = null, List<string> modmailMessagesMod = null, 
            List<string> modmailMessagesUser = null, List<string> modmailMessagesUnread = null, List<string> liveThread = null, List<string> liveThreadContributors = null, 
            List<string> liveThreadUpdates = null, List<string> postData = null, List<string> postScore = null, List<string> postHistory = null, List<string> commentHistory = null)
        {
            BestPosts = bestPosts ?? new List<string>();
            HotPosts = hotPosts ?? new List<string>();
            NewPosts = newPosts ?? new List<string>();
            RisingPosts = risingPosts ?? new List<string>();
            TopPosts = topPosts ?? new List<string>();
            ControversialPosts = controversialPosts ?? new List<string>();
            ModQueuePosts = modQueuePosts ?? new List<string>();
            ModQueueReportsPosts = modQueueReportsPosts ?? new List<string>();
            ModQueueSpamPosts = modQueueSpamPosts ?? new List<string>();
            ModQueueUnmoderatedPosts = modQueueUnmoderatedPosts ?? new List<string>();
            ModQueueEditedPosts = modQueueEditedPosts ?? new List<string>();
            PrivateMessagesInbox = privateMessagesInbox ?? new List<string>();
            PrivateMessagesUnread = privateMessagesUnread ?? new List<string>();
            PrivateMessagesSent = privateMessagesSent ?? new List<string>();
            ConfidenceComments = confidenceComments ?? new List<string>();
            TopComments = topComments ?? new List<string>();
            NewComments = newComments ?? new List<string>();
            ControversialComments = controversialComments ?? new List<string>();
            OldComments = oldComments ?? new List<string>();
            RandomComments = randomComments ?? new List<string>();
            QAComments = qaComments ?? new List<string>();
            LiveComments = liveComments ?? new List<string>();
            WikiPages = wikiPages ?? new List<string>();
            WikiPage = wikiPage ?? new List<string>();
            ModmailMessagesRecent = modmailMessagesRecent ?? new List<string>();
            ModmailMessagesMod = modmailMessagesMod ?? new List<string>();
            ModmailMessagesUser = modmailMessagesUser ?? new List<string>();
            ModmailMessagesUnread = modmailMessagesUnread ?? new List<string>();
            LiveThread = liveThread ?? new List<string>();
            LiveThreadContributors = liveThreadContributors ?? new List<string>();
            LiveThreadUpdates = liveThreadUpdates ?? new List<string>();
            PostData = postData ?? new List<string>();
            PostScore = postScore ?? new List<string>();
            PostHistory = postHistory ?? new List<string>();
            CommentHistory = commentHistory ?? new List<string>();
        }

        public ref List<string> Get(string key)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "BestPosts":
                    return ref BestPosts;
                case "HotPosts":
                    return ref HotPosts;
                case "NewPosts":
                    return ref NewPosts;
                case "RisingPosts":
                    return ref RisingPosts;
                case "TopPosts":
                    return ref TopPosts;
                case "ControversialPosts":
                    return ref ControversialPosts;
                case "ModQueuePosts":
                    return ref ModQueuePosts;
                case "ModQueueReportsPosts":
                    return ref ModQueueReportsPosts;
                case "ModQueueSpamPosts":
                    return ref ModQueueSpamPosts;
                case "ModQueueUnmoderatedPosts":
                    return ref ModQueueUnmoderatedPosts;
                case "ModQueueEditedPosts":
                    return ref ModQueueEditedPosts;
                case "PrivateMessagesInbox":
                    return ref PrivateMessagesInbox;
                case "PrivateMessagesUnread":
                    return ref PrivateMessagesUnread;
                case "PrivateMessagesSent":
                    return ref PrivateMessagesSent;
                case "ConfidenceComments":
                    return ref ConfidenceComments;
                case "TopComments":
                    return ref TopComments;
                case "NewComments":
                    return ref NewComments;
                case "ControversialComments":
                    return ref ControversialComments;
                case "OldComments":
                    return ref OldComments;
                case "RandomComments":
                    return ref RandomComments;
                case "QAComments":
                    return ref QAComments;
                case "LiveComments":
                    return ref LiveComments;
                case "WikiPages":
                    return ref WikiPages;
                case "WikiPage":
                    return ref WikiPage;
                case "ModmailMessagesRecent":
                    return ref ModmailMessagesRecent;
                case "ModmailMessagesMod":
                    return ref ModmailMessagesMod;
                case "ModmailMessagesUser":
                    return ref ModmailMessagesUser;
                case "ModmailMessagesUnread":
                    return ref ModmailMessagesUnread;
                case "LiveThread":
                    return ref LiveThread;
                case "LiveThreadContributors":
                    return ref LiveThreadContributors;
                case "LiveThreadUpdates":
                    return ref LiveThreadUpdates;
                case "PostData":
                    return ref PostData;
                case "PostScore":
                    return ref PostScore;
                case "PostHistory":
                    return ref PostHistory;
                case "CommentHistory":
                    return ref CommentHistory;
            }
        }
        
        public void Add(MonitoringSnapshot monitoringSnapshot)
        {
            if (monitoringSnapshot != null)
            {
                Add(monitoringSnapshot.BestPosts, ref BestPosts);
                Add(monitoringSnapshot.HotPosts, ref HotPosts);
                Add(monitoringSnapshot.NewPosts, ref NewPosts);
                Add(monitoringSnapshot.RisingPosts, ref RisingPosts);
                Add(monitoringSnapshot.TopPosts, ref TopPosts);
                Add(monitoringSnapshot.ControversialPosts, ref ControversialPosts);
                Add(monitoringSnapshot.ModQueuePosts, ref ModQueuePosts);
                Add(monitoringSnapshot.ModQueueReportsPosts, ref ModQueueReportsPosts);
                Add(monitoringSnapshot.ModQueueSpamPosts, ref ModQueueSpamPosts);
                Add(monitoringSnapshot.ModQueueUnmoderatedPosts, ref ModQueueUnmoderatedPosts);
                Add(monitoringSnapshot.ModQueueEditedPosts, ref ModQueueEditedPosts);
                Add(monitoringSnapshot.PrivateMessagesInbox, ref PrivateMessagesInbox);
                Add(monitoringSnapshot.PrivateMessagesUnread, ref PrivateMessagesUnread);
                Add(monitoringSnapshot.PrivateMessagesSent, ref PrivateMessagesSent);
                Add(monitoringSnapshot.ConfidenceComments, ref ConfidenceComments);
                Add(monitoringSnapshot.TopComments, ref TopComments);
                Add(monitoringSnapshot.NewComments, ref NewComments);
                Add(monitoringSnapshot.ControversialComments, ref ControversialComments);
                Add(monitoringSnapshot.OldComments, ref OldComments);
                Add(monitoringSnapshot.RandomComments, ref RandomComments);
                Add(monitoringSnapshot.QAComments, ref QAComments);
                Add(monitoringSnapshot.LiveComments, ref LiveComments);
                Add(monitoringSnapshot.WikiPages, ref WikiPages);
                Add(monitoringSnapshot.WikiPage, ref WikiPage);
                Add(monitoringSnapshot.ModmailMessagesRecent, ref ModmailMessagesRecent);
                Add(monitoringSnapshot.ModmailMessagesMod, ref ModmailMessagesMod);
                Add(monitoringSnapshot.ModmailMessagesUser, ref ModmailMessagesUser);
                Add(monitoringSnapshot.ModmailMessagesUnread, ref ModmailMessagesUnread);
                Add(monitoringSnapshot.LiveThread, ref LiveThread);
                Add(monitoringSnapshot.LiveThreadContributors, ref LiveThreadContributors);
                Add(monitoringSnapshot.LiveThreadUpdates, ref LiveThreadUpdates);
                Add(monitoringSnapshot.PostData, ref PostData);
                Add(monitoringSnapshot.PostScore, ref PostScore);
                Add(monitoringSnapshot.PostHistory, ref PostHistory);
                Add(monitoringSnapshot.CommentHistory, ref CommentHistory);
            }
        }

        private void Add(List<string> addList, ref List<string> dest)
        {
            foreach (string item in addList)
            {
                if (!dest.Contains(item))
                {
                    dest.Add(item);
                }
            }
        }

        public void Remove(MonitoringSnapshot monitoringSnapshot)
        {
            if (monitoringSnapshot != null)
            {
                Remove(monitoringSnapshot.BestPosts, ref BestPosts);
                Remove(monitoringSnapshot.HotPosts, ref HotPosts);
                Remove(monitoringSnapshot.NewPosts, ref NewPosts);
                Remove(monitoringSnapshot.RisingPosts, ref RisingPosts);
                Remove(monitoringSnapshot.TopPosts, ref TopPosts);
                Remove(monitoringSnapshot.ControversialPosts, ref ControversialPosts);
                Remove(monitoringSnapshot.ModQueuePosts, ref ModQueuePosts);
                Remove(monitoringSnapshot.ModQueueReportsPosts, ref ModQueueReportsPosts);
                Remove(monitoringSnapshot.ModQueueSpamPosts, ref ModQueueSpamPosts);
                Remove(monitoringSnapshot.ModQueueUnmoderatedPosts, ref ModQueueUnmoderatedPosts);
                Remove(monitoringSnapshot.ModQueueEditedPosts, ref ModQueueEditedPosts);
                Remove(monitoringSnapshot.PrivateMessagesInbox, ref PrivateMessagesInbox);
                Remove(monitoringSnapshot.PrivateMessagesUnread, ref PrivateMessagesUnread);
                Remove(monitoringSnapshot.PrivateMessagesSent, ref PrivateMessagesSent);
                Remove(monitoringSnapshot.ConfidenceComments, ref ConfidenceComments);
                Remove(monitoringSnapshot.TopComments, ref TopComments);
                Remove(monitoringSnapshot.NewComments, ref NewComments);
                Remove(monitoringSnapshot.ControversialComments, ref ControversialComments);
                Remove(monitoringSnapshot.OldComments, ref OldComments);
                Remove(monitoringSnapshot.RandomComments, ref RandomComments);
                Remove(monitoringSnapshot.QAComments, ref QAComments);
                Remove(monitoringSnapshot.LiveComments, ref LiveComments);
                Remove(monitoringSnapshot.WikiPages, ref WikiPages);
                Remove(monitoringSnapshot.WikiPage, ref WikiPage);
                Remove(monitoringSnapshot.ModmailMessagesRecent, ref ModmailMessagesRecent);
                Remove(monitoringSnapshot.ModmailMessagesMod, ref ModmailMessagesMod);
                Remove(monitoringSnapshot.ModmailMessagesUser, ref ModmailMessagesUser);
                Remove(monitoringSnapshot.ModmailMessagesUnread, ref ModmailMessagesUnread);
                Remove(monitoringSnapshot.LiveThread, ref LiveThread);
                Remove(monitoringSnapshot.LiveThreadContributors, ref LiveThreadContributors);
                Remove(monitoringSnapshot.LiveThreadUpdates, ref LiveThreadUpdates);
                Remove(monitoringSnapshot.PostData, ref PostData);
                Remove(monitoringSnapshot.PostScore, ref PostScore);
                Remove(monitoringSnapshot.PostHistory, ref PostHistory);
                Remove(monitoringSnapshot.CommentHistory, ref CommentHistory);
            }
        }

        private void Remove(List<string> removeList, ref List<string> dest)
        {
            foreach (string item in removeList)
            {
                if (dest.Contains(item))
                {
                    dest.Remove(item);
                }
            }
        }

        public int Count()
        {
            return (BestPosts.Count
                + HotPosts.Count
                + NewPosts.Count
                + RisingPosts.Count
                + ControversialPosts.Count
                + ModQueuePosts.Count
                + ModQueueReportsPosts.Count
                + ModQueueSpamPosts.Count
                + ModQueueUnmoderatedPosts.Count
                + ModQueueEditedPosts.Count
                + PrivateMessagesInbox.Count
                + PrivateMessagesUnread.Count
                + PrivateMessagesSent.Count
                + ConfidenceComments.Count
                + TopComments.Count
                + NewComments.Count
                + ControversialComments.Count
                + OldComments.Count
                + RandomComments.Count
                + QAComments.Count
                + LiveComments.Count
                + WikiPages.Count
                + WikiPage.Count
                + ModmailMessagesRecent.Count
                + ModmailMessagesMod.Count
                + ModmailMessagesUser.Count
                + ModmailMessagesUnread.Count
                + LiveThread.Count
                + LiveThreadContributors.Count
                + LiveThreadUpdates.Count
                + PostData.Count
                + PostScore.Count
                + PostHistory.Count
                + CommentHistory.Count);
        }
    }
}

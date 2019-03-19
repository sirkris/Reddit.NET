using ELIZA.NET;
using Reddit;
using Reddit.Controllers.EventArgs;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ELIZA
{
    class ELIZA
    {
        private readonly ELIZALib Eliza;
        private readonly RedditAPI Reddit;
        public Dictionary<string, DateTime> ActiveSessions;

        public bool Stop;

        // Load ELIZA script file and instantiate our libraries.  --Kris
        public ELIZA()
        {
            string json = null;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DOCTOR.json"))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    json = streamReader.ReadToEnd();
                }
            }

            Eliza = new ELIZALib(json);
            Reddit = new RedditAPI("YourAppID", "YourRefreshToken");
            ActiveSessions = new Dictionary<string, DateTime>();
        }

        // Call this method to run the bot.  Set Stop to true to terminate.  --Kris
        public void Run()
        {
            Stop = false;

            // Monitor the bot user's private messages and respond accordingly.  --Kris
            Reddit.Account.Messages.MonitorUnread(15000);  // Check for new messages every 15 seconds.  You can change this or remove entirely to let Reddit.NET manage the delays.
            Reddit.Account.Messages.UnreadUpdated += C_UnreadMessagesUpdated;

            while (!Stop) { }  // Everything else happens in the callback function below when new messages are received.  --Kris

            // Stop monitoring and terminate.  --Kris
            Reddit.Account.Messages.UnreadUpdated -= C_UnreadMessagesUpdated;
            Reddit.Account.Messages.MonitorUnread();
        }

        // Callback function that's triggered whenever one or more new messages come in.  --Kris
        private void C_UnreadMessagesUpdated(object sender, MessagesUpdateEventArgs e)
        {
            if (e.NewMessages.Count > 0)
            {
                // Mark all messages as read now so they don't come up multiple times.  --Kris
                Reddit.Account.Messages.MarkAllRead();

                foreach (Message message in e.NewMessages)
                {
                    // If author has an active session, reply to the content.  Otherwise, respond with greeting.  --Kris
                    string response;
                    if (!ActiveSessions.ContainsKey(message.Author))
                    {
                        ActiveSessions.Add(message.Author, DateTime.Now);
                        response = Eliza.Session.GetGreeting();  // These greetings are really generic so feel free to replace this with your own custom greeting string.  --Kris
                    }
                    else
                    {
                        if (message.Body.Contains("bye", StringComparison.OrdinalIgnoreCase)
                            || message.Body.Contains("farewell", StringComparison.OrdinalIgnoreCase)
                            || message.Body.Equals("exit", StringComparison.OrdinalIgnoreCase)
                            || message.Body.Equals("quit", StringComparison.OrdinalIgnoreCase))
                        {
                            ActiveSessions.Remove(message.Author);
                        }

                        response = Eliza.GetResponse(message.Body);
                    }

                    // Send the Reddit response.  --Kris
                    Reddit.Account.Messages.Compose(message.Author, message.Subject, response);
                }
            }
        }
    }
}

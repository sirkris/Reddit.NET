# ELIZA Chatbot

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

### [ELIZA.NET](https://github.com/sirkris/ELIZA.NET)

## Overview

This is a simple chatbot that monitors a designated Reddit bot account's new messages and sends replies.  The responses are generated using a recreation of an [early AI algorithm](https://en.wikipedia.org/wiki/ELIZA) written in 1966.  As such, don't expect it to be anywhere near as accurate or human-like as more modern AI chatbots.  

ELIZA is basically just an elaborate pattern-matching toy, so inputs should be limited to a single short sentence each for best results.  All punctuation is ignored.

## Live Demo

If you'd like to see what this looks like in action, try sending a private message to [u/ElizaRobot](/u/ElizaRobot).  

You'll probably get faster results if you create your own, though, as I'm not currently running this 24/7.  When she is running, she checks for new messages every 15 seconds and responds immediately.

## Library Installation

In the NuGet Package Manager console:

    Install-Package ELIZA.NET
    
    Install-Package Reddit

## The Code

For best results, I recommend that you add the scripts/DOCTOR/DOCTOR.json file from the ELIZA.NET repo as an embedded resource in your project, as you will need to point to that file in order for the ELIZA.NET library to properly function.  This example assumes you took that recommended step.

Note that you may want to add some exception handling to account for Reddit's notorious service outages and whatnot.  I tried to keep this example as simple and to-the-point as possible.

```c#
using ELIZA.NET;
using Reddit;
using Reddit.Controllers.EventArgs;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

...

class ELIZA
{
    private readonly ELIZALib Eliza;
    private readonly RedditClient Reddit;
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
        Reddit = new RedditClient("YourAppID", "YourRefreshToken");
        ActiveSessions = new Dictionary<string, DateTime>();
    }
    
    // Call this method to run the bot.  Set Stop to true to terminate.  --Kris
    public void Run()
    {
        Stop = false;
        
        // Monitor the bot user's private messages and respond accordingly.  --Kris
        Reddit.Account.Messages.MonitorUnread(15000);  // Check for new messages every 15 seconds.  You can change this or remove entirely to let Reddit.NET manage the delays.
        Reddit.Account.Messages.UnreadUpdated += C_UnreadMessagesUpdated;
        
        while ( !Stop ) { }  // Everything else happens in the callback function below when new messages are received.  --Kris
        
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
```

## Source File

[ELIZA.cs](src/ELIZA.cs)

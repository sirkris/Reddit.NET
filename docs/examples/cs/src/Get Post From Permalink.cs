using Reddit;
using Reddit.Controllers;
using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
	public class GetPost
	{
		private RedditClient Reddit { get; set; }
		
		public GetPost(string appId, string refreshToken)
		{
			Reddit = new RedditClient(appId, refreshToken);
		}
		
		public Post FromPermalink(string permalink)
		{
			// Get the ID from the permalink, then preface it with "t3_" to convert it to a Reddit fullname.  --Kris
			Match match = Regex.Match(permalink, @"\/comments\/([a-z0-9]+)\/");
			
			string postFullname = "t3_" + (match != null && match.Groups != null && match.Groups.Count >= 2 
				? match.Groups[1].Value 
				: "");
			if (postFullname.Equals("t3_"))
			{
				throw new Exception("Unable to extract ID from permalink.");
			}
			
			// Retrieve the post and return the result.  --Kris
			return Reddit.Post(Reddit.Models, postFullname).About();
		}
	}
}

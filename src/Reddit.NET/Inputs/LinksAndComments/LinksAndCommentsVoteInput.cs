using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsVoteInput : LinksAndCommentsIdInput
    {
        /// <summary>
        /// vote direction. one of (1, 0, -1)
        /// </summary>
        public int dir { get; set; }

        /// <summary>
        /// an integer greater than 1
        /// </summary>
        public int rank { get; set; }

        /// <summary>
        /// Cast a vote on a thing.
        /// id should be the fullname of the Link or Comment to vote on.
        /// dir indicates the direction of the vote. Voting 1 is an upvote, -1 is a downvote, and 0 is equivalent to "un-voting" by clicking again on a highlighted arrow.
        /// Note: votes must be cast by humans.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="dir">vote direction. one of (1, 0, -1)</param>
        /// <param name="rank">an integer greater than 1</param>
        public LinksAndCommentsVoteInput(string id = "", int dir = 0, int rank = 2)
        {
            this.id = id;
            this.dir = dir;
            this.rank = rank;
        }
    }
}

using System;

namespace Reddit.Inputs.Users
{
    [Serializable]
    public class UsersReportUserInput : BaseInput
    {
        /// <summary>
        /// JSON data
        /// </summary>
        string details { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        string reason { get; set; }

        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        string user { get; set; }

        /// <summary>
        /// Report a user. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        /// <param name="details"JSON data></param>
        public UsersReportUserInput(string user = "", string reason = "", string details = "{}")
        {
            this.user = user;
            this.reason = reason;
            this.details = details;
        }
    }
}

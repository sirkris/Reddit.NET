using System;

namespace Reddit.Inputs.Users
{
    [Serializable]
    public class UsersBlockUserInput : BaseInput
    {
        /// <summary>
        /// fullname of an account
        /// </summary>
        public string account_id { get; set; }

        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// For blocking a user.
        /// </summary>
        /// <param name="accountId">fullname of an account</param>
        /// <param name="name">A valid, existing reddit username</param>
        public UsersBlockUserInput(string accountId = "", string name = "")
        {
            account_id = accountId;
            this.name = name;
        }
    }
}

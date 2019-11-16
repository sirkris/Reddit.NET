using System;

namespace Reddit.Inputs.Wiki
{
    [Serializable]
    public class WikiUpdatePermissionsInput : BaseInput
    {
        /// <summary>
        /// boolean value (true = appear in /wiki/pages, false = don't appear in /wiki/pages)
        /// </summary>
        public bool listed { get; set; }

        /// <summary>
        /// an integer (0 = use wiki perms, 1 = only approved users may edit, 2 = only mods may edit or view)
        /// </summary>
        public int permlevel { get; set; }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="listed">boolean value (true = appear in /wiki/pages, false = don't appear in /wiki/pages)</param>
        /// <param name="permLevel">an integer (0 = use wiki perms, 1 = only approved users may edit, 2 = only mods may edit or view)</param>
        public WikiUpdatePermissionsInput(bool listed = true, int permLevel = 0)
        {
            this.listed = listed;
            permlevel = permLevel;
        }
    }
}

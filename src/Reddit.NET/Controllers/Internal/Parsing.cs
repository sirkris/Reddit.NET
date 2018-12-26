using System.Web;

namespace Reddit.Controllers.Internal
{
    public static class Parsing
    {
        public static string HtmlEncode(string str)
        {
            return (!string.IsNullOrWhiteSpace(str) ? HttpUtility.HtmlEncode(str) : str);
        }

        public static string HtmlDecode(string str)
        {
            return (!string.IsNullOrWhiteSpace(str) ? HttpUtility.HtmlDecode(str) : str);
        }
    }
}

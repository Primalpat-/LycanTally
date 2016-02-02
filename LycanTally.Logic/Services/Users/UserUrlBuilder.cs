using LycanTally.Core.Constants;

namespace LycanTally.Logic.Services.Users
{
    public class UserUrlBuilder
    {
        public static string GetUserUrl(string userName)
        {
            return Constants.BaseUserUrl + "?name=" + userName;
        }
    }
}

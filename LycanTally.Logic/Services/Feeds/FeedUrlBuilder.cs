using LycanTally.Core.Constants;
using LycanTally.Core.Contexts;
using System.Linq;
using Z.Core.Extensions;

namespace LycanTally.Logic.Services.Feeds
{
    public class FeedUrlBuilder
    {
        private readonly ILycanTallyContext Db;

        public FeedUrlBuilder(ILycanTallyContext db)
        {
            Db = db;
        }

        public string GetFeedUrl(int threadID)
        {
            var minArticleID = Db.Articles.Where(a => a.ThreadID == threadID)
                                          .Select(a => a.ID)
                                          .DefaultIfEmpty(0)
                                          .Max() + 1;

            return Constants.BaseFeedUrl + "?id=" + threadID + "&minarticleid=" + minArticleID;
        }
    }
}

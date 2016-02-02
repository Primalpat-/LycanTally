using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Z.Core.Extensions;

namespace LycanTally.Logic.Services.Articles
{
    public class ArticleSearchService
    {
        private readonly ILycanTallyContext Db;

        public ArticleSearchService(ILycanTallyContext db)
        {
            Db = db;
        }

        public List<Article> GetArticles(int threadID, string userFilter = "", string dayFilter = "")
        {
            List<string> users = new List<string>();
            List<int> days = new List<int>();
            int lowID = 0;
            int highID = int.MaxValue;

            if (userFilter.IsNotNullOrWhiteSpace())
                users = userFilter.Split(',').ToList();

            if (dayFilter.IsNotNullOrWhiteSpace())
            {
                days = dayFilter.Split(',').Select(int.Parse).ToList();
                lowID = GetArticleIdOfDawn(threadID, days.First());
                highID = GetArticleIdOfNextDaysDawn(threadID, days.Last());
            }

            return Db.Articles.Where(a => a.ThreadID == threadID &&
                                     a.ID >= lowID && a.ID <= highID &&
                                     (users.Contains(a.User.Name) || users.Count == 0))
                              .ToList();
        }

        //TODO - Cant think of a better functon name at the moment
        private int GetArticleIdOfDawn(int threadID, int dayNumber)
        {
            if (dayNumber > 0)
            {
                return Db.Articles.Where(a => a.ThreadID == threadID &&
                                        a.Subject != "bobtally help" &&
                                        a.Body.Contains("[dawn]"))
                                  .Select(a => a.ID)
                                  .ToList()[dayNumber - 1];
            }

            return 0;
        }

        //TODO - Cant think of a better function name at the moment
        private int GetArticleIdOfNextDaysDawn(int threadID, int dayNumber)
        {
            List<int> dawns = Db.Articles.Where(a => a.ThreadID == threadID &&
                                                a.Subject != "bobtally help" &&
                                                a.Body.Contains("[dawn]"))
                                         .Select(a => a.ID)
                                         .ToList();

            if (dawns.Count > dayNumber)
                return dawns[dayNumber];

            return int.MaxValue;
        }
    }
}

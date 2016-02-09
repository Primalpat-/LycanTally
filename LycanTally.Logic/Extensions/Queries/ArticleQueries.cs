using LycanTally.Core.Entities;
using System.Data.Entity;
using System.Linq;

namespace LycanTally.Logic.Extensions.Queries
{
    public static class ArticleQueries
    {
        public static int GetNumberOfArticles(this DbSet<Article> dbset, int threadID, int userID)
        {
            return dbset.Where(a => a.ThreadID == threadID && a.UserID == userID)
                        .Count();
        }
    }
}

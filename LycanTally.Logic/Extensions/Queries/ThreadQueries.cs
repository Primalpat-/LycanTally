using LycanTally.Core.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LycanTally.Logic.Extensions.Queries
{
    public static class ThreadQueries
    {
        public static List<User> GetUsers(this DbSet<Thread> dbset, int threadID)
        {
            return dbset.Where(t => t.ID == threadID)
                        .Select(t => t.Articles.GroupBy(a => a.User)
                                               .Select(a => a.FirstOrDefault())
                                               .Select(a => a.User)
                                               .ToList())
                        .FirstOrDefault();       
        }

        public static int GetNumberOfGameDays(this DbSet<Thread> dbset, int threadID)
        {
            return dbset.Where(t => t.ID == threadID)
                        .Select(t => t.Articles.Where(a => a.Subject != "bobtally help" &&
                                                      a.Body.Contains("[dawn]"))
                                               .Count())
                        .FirstOrDefault();
        }
    }
}

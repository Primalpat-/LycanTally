using Ether.Outcomes;
using LycanTally.Core.Entities;
using LycanTally.Logic.Services.Articles;
using LycanTally.Logic.Services.Threads;
using LycanTally.Logic.Services.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LycanTally.Logic.Services.Feeds
{
    public class FeedSaver
    {
        private readonly ThreadSavingService ThreadSaver;
        private readonly ArticleSavingService ArticleSaver;
        private readonly UserSaver UserSaver;

        public FeedSaver(ThreadSavingService threadSaver, ArticleSavingService articleSaver, UserSaver userSaver)
        {
            ThreadSaver = threadSaver;
            ArticleSaver = articleSaver;
            UserSaver = userSaver;
        }

        public IOutcome<IEnumerable<Article>> Save(string connectionID, Thread thread)
        {
            ThreadSaver.SaveThread(thread);
            ArticleSaver.SaveArticles(connectionID, thread.Articles.ToList());

            return Outcomes.Success<IEnumerable<Article>>()
                           .WithValue(thread.Articles);
        }
    }
}

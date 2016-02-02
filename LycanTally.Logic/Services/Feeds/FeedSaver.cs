using Ether.Outcomes;
using LycanTally.Logic.Services.Articles;
using LycanTally.Logic.Services.Threads;
using LycanTally.Logic.Services.Users;

namespace LycanTally.Logic.Services.Feeds
{
    public class FeedSaver
    {
        private readonly FeedReader FeedReader;
        private readonly ThreadSavingService ThreadSaver;
        private readonly ArticleSavingService ArticleSaver;
        private readonly UserSaver UserSaver;

        public FeedSaver(FeedReader feedReader, ThreadSavingService threadSaver, ArticleSavingService articleSaver, UserSaver userSaver)
        {
            FeedReader = feedReader;
            ThreadSaver = threadSaver;
            ArticleSaver = articleSaver;
            UserSaver = userSaver;
        }

        public IOutcome Save(string feedUrl)
        {
            var feedOutcome = FeedReader.ReadNewOrUpdatedFeed(feedUrl);

            if (feedOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(feedOutcome);

            ThreadSaver.SaveThread(feedOutcome.Value);
            ArticleSaver.SaveArticles(feedOutcome.Value.Articles);

            return Outcomes.Success();
        }
    }
}

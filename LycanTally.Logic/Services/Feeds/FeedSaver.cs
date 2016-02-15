using Ether.Outcomes;
using LycanTally.Core.Entities;
using LycanTally.Logic.Services.Articles;
using LycanTally.Logic.Services.Threads;
using LycanTally.Logic.Services.Users;
using System.Collections.Generic;

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

        public IOutcome<IEnumerable<Article>> Save(string feedUrl)
        {
            var feedOutcome = FeedReader.ReadNewOrUpdatedFeed(feedUrl);

            if (feedOutcome.Failure)
                return Outcomes.Failure<IEnumerable<Article>>()
                               .WithMessagesFrom(feedOutcome);

            if (!ThreadSaver.ThreadNeededSaving(feedOutcome.Value))
                ArticleSaver.SaveArticles(feedOutcome.Value.Articles);

            return Outcomes.Success<IEnumerable<Article>>()
                           .WithValue(feedOutcome.Value.Articles);
        }
    }
}

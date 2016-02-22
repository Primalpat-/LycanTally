using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using LycanTally.Logic.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LycanTally.Logic.Services.Articles
{
    public class ArticleSavingService
    {
        private readonly ILycanTallyContext Db;

        public ArticleSavingService(ILycanTallyContext db)
        {
            Db = db;
        }

        public void SaveArticles(string connectionID, List<Article> articles)
        {
            Task saveTask = SaveArticlesAsync(articles);

            //we are progressing from 56 to 95 here
            for (int i = 56; i < 95; i++)
            {
                if (saveTask.IsCompleted)
                    break;

                System.Threading.Thread.Sleep(Convert.ToInt32(0.91 * articles.Count()));
                ProgressHub.SendMessage(connectionID, "Constructing Gallows...", i);
            }

            saveTask.Wait();
        }

        private async Task SaveArticlesAsync(List<Article> articles)
        {
            Db.Articles.AddRange(articles);
            await Db.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public int SaveArticle(Article article)
        {
            Article existingArticle = Db.Articles.FirstOrDefault(t => t.ID == article.ID);

            if (existingArticle != null)
                return article.ID;

            Db.Articles.Add(article);
            Db.SaveChanges();

            return article.ID;
        }
    }
}

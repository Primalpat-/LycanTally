using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LycanTally.Logic.Services.Articles
{
    public class ArticleSavingService
    {
        private readonly ILycanTallyContext Db;

        public ArticleSavingService(ILycanTallyContext db)
        {
            Db = db;
        }

        public void SaveArticles(IEnumerable<Article> articles)
        {
            //This is the fastest method I've found for loading the articles into the DB
            //I have tried checking for existing ones, which seemed to slow it by 100%
            //Also tried saving each on individually, which slowed it ~120%
            foreach(Article article in articles)
                Db.Articles.Add(article);
                
            Db.SaveChanges();
        }

        public int SaveArticle(Article article)
        {
            Article existingArticle = Db.Articles.Where(t => t.ID == article.ID)
                                                 .FirstOrDefault();

            if (existingArticle != null)
                return article.ID;

            Db.Articles.Add(article);
            Db.SaveChanges();

            return article.ID;
        }
    }
}

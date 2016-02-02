using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using System;
using System.Collections;
using System.Xml.Linq;

namespace LycanTally.Core.Threads
{
    public class ThreadParser
    {
        private readonly ILycanTallyContext Db;

        public ThreadParser(ILycanTallyContext db)
        {
            Db = db;
        }

        public Thread Parse(XDocument doc)
        {
            var thread = LoadThreadValues(doc);
            var articles = doc.Descendants("article");

            foreach (var article in articles)
            {
                var post = LoadArticleValues(thread.ID, article);
                thread.Articles.Add(post);
            }

            return thread;
        }

        private Thread LoadThreadValues(XDocument doc)
        {
            Thread thread = new Thread();
            thread.ID = (int)doc.Element("thread").Attribute("id");
            thread.NumArticles = (int)doc.Element("thread").Attribute("numarticles");
            thread.Link = doc.Element("thread").Attribute("link").Value;
            thread.TermsOfUse = doc.Element("thread").Attribute("termsofuse").Value;
            thread.Subject = doc.Element("thread").Element("subject").Value;

            Db.Threads.Add(thread);
            Db.SaveChanges();

            return thread;
        }

        private Article LoadArticleValues(int ThreadID, XElement article)
        {
            Article post = new Article();
            post.ID = (int)article.Attribute("id");
            post.ThreadID = ThreadID;
            post.UserName = article.Attribute("username").Value;
            post.Link = article.Attribute("link").Value;
            post.PostDate = DateTime.Parse(article.Attribute("postdate").Value,
                                           System.Globalization.CultureInfo.CurrentCulture,
                                           System.Globalization.DateTimeStyles.AdjustToUniversal);
            post.EditDate = DateTime.Parse(article.Attribute("editdate").Value,
                                           System.Globalization.CultureInfo.CurrentCulture,
                                           System.Globalization.DateTimeStyles.AdjustToUniversal);
            post.NumEdits = (int)article.Attribute("numedits");
            post.Subject = article.Element("subject").Value;
            post.Body = article.Element("body").Value;

            Db.Articles.Add(post);
            Db.SaveChanges();

            return post;
        }
    }
}
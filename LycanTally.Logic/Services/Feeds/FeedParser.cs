using LycanTally.Core.Entities;
using LycanTally.Logic.Services.Users;
using LycanTally.Logic.SignalR;
using System;
using System.Linq;
using System.Xml.Linq;

namespace LycanTally.Logic.Services.Feeds
{
    public class FeedParser
    {
        private readonly UserSaver Saver;

        public FeedParser(UserSaver saver)
        {
            Saver = saver;
        }

        public Thread Parse(string connectionID, XDocument doc)
        {
            Thread thread = LoadThread(doc);
            var articles = doc.Descendants("article").ToList();

            for (int i = 0; i < articles.Count(); i++)
            {
                int progressCount = 10 + Convert.ToInt32((double)i / (double)articles.Count() * 46.00);
                ProgressHub.SendMessage(connectionID, "Sharpening Pitchforks...", progressCount);
                var article = LoadArticle(thread, articles[i]);
                thread.Articles.Add(article);
            }
                
            return thread;
        }

        private Thread LoadThread(XDocument doc)
        {
            Thread thread = new Thread();
            thread.ID = (int)doc.Element("thread").Attribute("id");
            thread.NumArticles = (int)doc.Element("thread").Attribute("numarticles");
            thread.Link = doc.Element("thread").Attribute("link").Value;
            thread.TermsOfUse = doc.Element("thread").Attribute("termsofuse").Value;
            thread.Subject = doc.Element("thread").Element("subject").Value;

            return thread;
        }

        private Article LoadArticle(Thread thread, XElement article)
        {
            Article post = new Article();
            post.ID = (int)article.Attribute("id");
            post.ThreadID = thread.ID;
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

            var userOutcome = Saver.GetNewOrUpdatedUser(article.Attribute("username").Value, post.PostDate);

            if (userOutcome.Failure)
                post.UserID = 0;

            post.UserID = userOutcome.Value.ID;

            return post;
        }
    }
}
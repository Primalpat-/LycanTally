using LycanTally.Core.Constants;
using LycanTally.Logic.Services.Articles;
using LycanTally.Logic.Services.Feeds;
using LycanTally.Web.Models;
using System;
using System.Web.Mvc;

namespace LycanTally.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly FeedUrlBuilder UrlBuilder;
        private readonly FeedSaver Saver;
        private readonly ArticleSearchService ArticleSearcher;

        public HomeController(FeedUrlBuilder urlBuilder, FeedSaver saver, ArticleSearchService articleSearcher)
        {
            UrlBuilder = urlBuilder;
            Saver = saver;
            ArticleSearcher = articleSearcher;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RetrieveMetrics(HomeVM model)
        {
            string feedUrl = UrlBuilder.GetFeedUrl(model.ThreadID);

            var saveOutcome = Saver.Save(feedUrl);

            if (saveOutcome.Failure)
                return new HttpStatusCodeResult(500, saveOutcome.ToMultiLine(Environment.NewLine));

            var articles = ArticleSearcher.GetArticles(model.ThreadID, "", "0");

            return Json(saveOutcome, JsonRequestBehavior.AllowGet);
        }
    }
}
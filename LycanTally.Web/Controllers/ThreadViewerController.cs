using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using LycanTally.Logic.Extensions.Queries;
using LycanTally.Logic.Services.Articles;
using LycanTally.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LycanTally.Web.Controllers
{
    public class ThreadViewerController : Controller
    {
        private readonly ILycanTallyContext Db;
        private readonly ArticleSearchService ArticleSearcher;

        public ThreadViewerController(ILycanTallyContext db, ArticleSearchService articleSearcher)
        {
            Db = db;
            ArticleSearcher = articleSearcher;
        }

        public ActionResult Index(int threadID)
        {
            var model = new ThreadViewerVM();

            Db.Configuration.ProxyCreationEnabled = false;
            model.ThreadID = threadID;
            model.Thread = Db.Threads.Where(t => t.ID == threadID)
                                     .FirstOrDefault();
            model.userItems = GetUserItems(threadID);
            model.totalDays = Db.Threads.GetNumberOfGameDays(threadID);

            return View(model);
        }

        public ActionResult GetArticles(int threadID, string userFilter, string dayFilter)
        {
            ArticlesVM model = new ArticlesVM();
            model.Articles = ArticleSearcher.GetArticles(threadID, userFilter, dayFilter);
            return PartialView("_Articles", model);
        }

        private List<UserItemVM> GetUserItems(int threadID)
        {
            List<UserItemVM> userItems = new List<UserItemVM>();

            foreach (User user in Db.Threads.GetUsers(threadID))
            {
                UserItemVM model = new UserItemVM();
                model.userName = user.Name;
                model.numArticles = Db.Articles.GetNumberOfArticles(threadID, user.ID);
                userItems.Add(model);
            }

            return userItems.OrderByDescending(i => i.numArticles).ToList();
        }
    }
}
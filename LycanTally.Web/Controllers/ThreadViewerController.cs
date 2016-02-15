using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using LycanTally.Logic.Extensions.Queries;
using LycanTally.Logic.Services.Articles;
using LycanTally.Logic.Services.Feeds;
using LycanTally.Logic.Services.Roles;
using LycanTally.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Z.Core.Extensions;

namespace LycanTally.Web.Controllers
{
    public class ThreadViewerController : Controller
    {
        private readonly ILycanTallyContext Db;
        private readonly FeedUrlBuilder UrlBuilder;
        private readonly FeedSaver Saver;
        private readonly RoleSavingService RoleSaver;
        private readonly ArticleSearchService ArticleSearcher;

        public ThreadViewerController(ILycanTallyContext db, FeedUrlBuilder urlBuilder, FeedSaver saver, 
                                      RoleSavingService roleSaver, ArticleSearchService articleSearcher)
        {
            Db = db;
            UrlBuilder = urlBuilder;
            Saver = saver;
            RoleSaver = roleSaver;
            ArticleSearcher = articleSearcher;
        }

        public ActionResult Index(int id) //id should be our ThreadID
        {
            string feedUrl = UrlBuilder.GetFeedUrl(id);

            var saveOutcome = Saver.Save(feedUrl);

            if (saveOutcome.Failure)
                return new HttpStatusCodeResult(500, saveOutcome.ToMultiLine(Environment.NewLine));

            RoleSaver.SaveUsersRoles(saveOutcome.Value.AsQueryable());

            var model = new ThreadViewerVM();

            Db.Configuration.ProxyCreationEnabled = false;
            model.ThreadID = id;
            model.Thread = Db.Threads.Where(t => t.ID == id)
                                     .FirstOrDefault();
            model.userItems = GetUserItems(id);
            model.totalDays = Db.Threads.GetNumberOfGameDays(id);

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
                model.alignmentClass = GetUserRoleAlignmentClass(threadID, user.ID);
                model.numArticles = Db.Articles.GetNumberOfArticles(threadID, user.ID);
                userItems.Add(model);
            }

            return userItems.OrderByDescending(i => i.numArticles).ToList();
        }

        private string GetUserRoleAlignmentClass(int threadID, int userID)
        {
            User_Thread_Roles userRole = Db.User_Thread_Roles.Include(utr => utr.Role.Alignment)
                                                             .FirstOrDefault(utr => utr.ThreadID == threadID &&
                                                                             utr.UserID == userID);

            if (userRole.IsNull())
                return "default";

            return userRole.Role.Alignment.Class;
        }
    }
}
using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using LycanTally.Logic.Extensions.Queries;
using LycanTally.Logic.Services.Articles;
using LycanTally.Web.Models;
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
        private readonly ArticleSearchService ArticleSearcher;

        public ThreadViewerController(ILycanTallyContext db, ArticleSearchService articleSearcher)
        {
            Db = db;
            ArticleSearcher = articleSearcher;
        }

        public ActionResult Index(int id) //id should be our ThreadID
        {
            return View(new ThreadViewerVM() { ThreadID = id });
        }

        public ActionResult GetThread(int threadID)
        {
            Db.Configuration.ProxyCreationEnabled = false;
            var model = new ThreadVM()
            {
                Thread = Db.Threads.FirstOrDefault(t => t.ID == threadID),
                totalDays = Db.Threads.GetNumberOfGameDays(threadID),
                userItems = GetUserItems(threadID)
            };
            return PartialView("_Thread", model);
        }

        public ActionResult GetArticles(int threadID, string userFilter, string dayFilter)
        {
            ArticlesVM model = new ArticlesVM()
            {
                Articles = ArticleSearcher.GetArticles(threadID, userFilter, dayFilter)
            };
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
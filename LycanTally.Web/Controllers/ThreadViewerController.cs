using LycanTally.Core.Contexts;
using LycanTally.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace LycanTally.Web.Controllers
{
    public class ThreadViewerController : Controller
    {
        private readonly ILycanTallyContext Db;

        public ThreadViewerController(ILycanTallyContext db)
        {
            Db = db;
        }

        public ActionResult Index(int threadID)
        {
            var model = new ThreadViewerVM();
            model.Thread = Db.Threads.Where(t => t.ID == threadID)
                                     .FirstOrDefault();

            return View(model);
        }
    }
}
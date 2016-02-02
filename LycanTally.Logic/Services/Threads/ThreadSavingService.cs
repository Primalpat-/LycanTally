using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using System.Linq;

namespace LycanTally.Logic.Services.Threads
{
    public class ThreadSavingService
    {
        private readonly ILycanTallyContext Db;

        public ThreadSavingService(ILycanTallyContext db)
        {
            Db = db;
        }

        public int SaveThread(Thread thread)
        {
            Thread existingThread = Db.Threads.Where(t => t.ID == thread.ID)
                                              .FirstOrDefault();

            if (existingThread != null)
                return thread.ID;

            Db.Threads.Add(thread);
            Db.SaveChanges();

            return thread.ID;
        }
    }
}

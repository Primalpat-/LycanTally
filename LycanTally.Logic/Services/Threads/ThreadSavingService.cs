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

        public bool ThreadNeededSaving(Thread thread)
        {
            Thread existingThread = Db.Threads.Where(t => t.ID == thread.ID)
                                              .FirstOrDefault();

            if (existingThread != null)
                return false;

            SaveThread(thread);
            return true;
        }

        private int SaveThread(Thread thread)
        {
            Db.Threads.Add(thread);
            Db.SaveChanges();

            return thread.ID;
        }
    }
}

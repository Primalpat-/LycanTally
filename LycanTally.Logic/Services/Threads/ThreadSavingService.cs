using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using System.Linq;
using Z.Core.Extensions;

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
            Thread existingThread = Db.Threads.FirstOrDefault(t => t.ID == thread.ID);

            if (existingThread.IsNotNull())
                return existingThread.ID;

            Thread threadToSave = new Thread()
            {
                ID = thread.ID,
                NumArticles = thread.NumArticles,
                Link = thread.Link,
                TermsOfUse = thread.TermsOfUse,
                Subject = thread.Subject
            };

            Db.Threads.Add(threadToSave);
            Db.SaveChanges();

            return threadToSave.ID;
        }
    }
}

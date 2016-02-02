using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using LycanTally.Data.Models.Mapping;
using System.Data.Entity;

namespace LycanTally.Data.Models.Context
{
    public partial class LycanTallyContext : DbContext, ILycanTallyContext
    {
        static LycanTallyContext()
        {
            Database.SetInitializer<LycanTallyContext>(null);
        }

        public LycanTallyContext()
            : base("Name=LycanTallyContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Thread> Threads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new ThreadMap());
        }
    }
}

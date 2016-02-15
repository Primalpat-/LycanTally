using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using LycanTally.Data.Models.Mapping;
using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;

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

        public DbSet<Alignment> Alignments { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<User_Thread_Roles> User_Thread_Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlignmentMap());
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new ThreadMap());
            modelBuilder.Configurations.Add(new User_Thread_RolesMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}

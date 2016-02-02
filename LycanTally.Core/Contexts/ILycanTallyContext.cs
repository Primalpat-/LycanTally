using LycanTally.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace LycanTally.Core.Contexts
{
    public interface ILycanTallyContext
    {
        DbSet<Article> Articles { get; set; }
        DbSet<Entities.Thread> Threads { get; set; }
        DbSet<User> Users { get; set; }
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        DbSet Set(Type entityType);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbEntityEntry Entry(object entity);
        void Dispose();
        string ToString();
        bool Equals(object obj);
        int GetHashCode();
        Type GetType();
    }
}

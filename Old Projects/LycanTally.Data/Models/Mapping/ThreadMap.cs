using LycanTally.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LycanTally.Data.Models.Mapping
{
    public class ThreadMap : EntityTypeConfiguration<Thread>
    {
        public ThreadMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Link)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.TermsOfUse)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Subject)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Threads");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.NumArticles).HasColumnName("NumArticles");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.TermsOfUse).HasColumnName("TermsOfUse");
            this.Property(t => t.Subject).HasColumnName("Subject");
        }
    }
}

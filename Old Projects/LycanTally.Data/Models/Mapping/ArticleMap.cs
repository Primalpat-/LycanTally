using LycanTally.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LycanTally.Data.Models.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Link)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Subject)
                .HasMaxLength(200);

            this.Property(t => t.Body)
                .HasMaxLength(5000);

            // Table & Column Mappings
            this.ToTable("Articles");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ThreadID).HasColumnName("ThreadID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.PostDate).HasColumnName("PostDate");
            this.Property(t => t.EditDate).HasColumnName("EditDate");
            this.Property(t => t.NumEdits).HasColumnName("NumEdits");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");

            // Relationships
            this.HasRequired(t => t.Thread)
                .WithMany(t => t.Articles)
                .HasForeignKey(d => d.ThreadID);

        }
    }
}

using LycanTally.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LycanTally.Data.Models.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Aliases)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Roles");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Aliases).HasColumnName("Aliases");
            this.Property(t => t.AlignmentID).HasColumnName("AlignmentID");

            // Relationships
            this.HasRequired(t => t.Alignment)
                .WithMany(t => t.Roles)
                .HasForeignKey(d => d.AlignmentID);

        }
    }
}

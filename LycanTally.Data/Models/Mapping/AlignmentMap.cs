using LycanTally.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LycanTally.Data.Models.Mapping
{
    public class AlignmentMap : EntityTypeConfiguration<Alignment>
    {
        public AlignmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Class)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Alignments");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Class).HasColumnName("Class");
        }
    }
}

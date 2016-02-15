using LycanTally.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LycanTally.Data.Models.Mapping
{
    public class User_Thread_RolesMap : EntityTypeConfiguration<User_Thread_Roles>
    {
        public User_Thread_RolesMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Table & Column Mappings
            this.ToTable("User_Thread_Roles");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ThreadID).HasColumnName("ThreadID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.User_Thread_Roles)
                .HasForeignKey(d => d.RoleID);
            this.HasRequired(t => t.Thread)
                .WithMany(t => t.User_Thread_Roles)
                .HasForeignKey(d => d.ThreadID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.User_Thread_Roles)
                .HasForeignKey(d => d.UserID);

        }
    }
}

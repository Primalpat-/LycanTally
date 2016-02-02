using LycanTally.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LycanTally.Data.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TermsOfUse)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.AvatarLink)
                .HasMaxLength(500);

            this.Property(t => t.StateOrProvince)
                .HasMaxLength(50);

            this.Property(t => t.Country)
                .HasMaxLength(50);

            this.Property(t => t.WebAddress)
                .HasMaxLength(50);

            this.Property(t => t.XboxAccount)
                .HasMaxLength(50);

            this.Property(t => t.WiiAccount)
                .HasMaxLength(50);

            this.Property(t => t.PsnAccount)
                .HasMaxLength(50);

            this.Property(t => t.BattleNetAccount)
                .HasMaxLength(50);

            this.Property(t => t.SteamAccount)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TermsOfUse).HasColumnName("TermsOfUse");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.AvatarLink).HasColumnName("AvatarLink");
            this.Property(t => t.YearRegistered).HasColumnName("YearRegistered");
            this.Property(t => t.LastLogin).HasColumnName("LastLogin");
            this.Property(t => t.StateOrProvince).HasColumnName("StateOrProvince");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.WebAddress).HasColumnName("WebAddress");
            this.Property(t => t.XboxAccount).HasColumnName("XboxAccount");
            this.Property(t => t.WiiAccount).HasColumnName("WiiAccount");
            this.Property(t => t.PsnAccount).HasColumnName("PsnAccount");
            this.Property(t => t.BattleNetAccount).HasColumnName("BattleNetAccount");
            this.Property(t => t.SteamAccount).HasColumnName("SteamAccount");
            this.Property(t => t.TradeRating).HasColumnName("TradeRating");
        }
    }
}

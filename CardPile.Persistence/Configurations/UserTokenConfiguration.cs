using CardPile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPile.Persistence.Configurations
{

    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {

        #region - - - - - - IEntityTypeConfiguration Implementation - - - - - -

        public void Configure(EntityTypeBuilder<UserToken> entity)
        {
            entity.ToTable("UserToken");

            // Username
            entity.HasKey(a => a.Username);
            entity.Property(a => a.Username)
                .IsRequired()
                .HasColumnName("Username");
            entity.HasIndex(a => a.Username)
                .HasDatabaseName("UQ_UserToken")
                .IsUnique();

            // Token
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsRequired();
        }

        #endregion IEntityTypeConfiguration Implementation

    }

}

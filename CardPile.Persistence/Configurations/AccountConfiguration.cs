using CardPile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPile.Persistence.Configurations
{

    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {

        #region - - - - - - IEntityTypeConfiguration Implementation - - - - - -

        public void Configure(EntityTypeBuilder<Account> entity)
        {
            entity.ToTable("Account");

            // AccountID
            entity.HasKey(a => a.AccountID);
            entity.Property(a => a.AccountID)
                  .HasColumnName("AccountID")
                  .ValueGeneratedOnAdd();

            // Password
            entity.OwnsOne(a => a.Password, password =>
            {
                password.Property<string>("m_Password")
                .HasColumnName("Password")
                .HasMaxLength(100)
                .IsRequired(false);
            });

            // UserName
            entity.Property(a => a.UserName)
                  .HasMaxLength(30)
                  .IsRequired();
            entity.HasIndex(a => a.UserName)
                  .HasDatabaseName("UQ_Account_UserName")
                  .IsUnique();

            // Email
            entity.Property(a => a.Email)
                  .HasMaxLength(250);
            entity.HasIndex(a => a.Email)
                  .HasDatabaseName("UQ_Account_Email")
                  .IsUnique();

            // GuestToken
            entity.Property(a => a.GuestToken);
        }

        #endregion IEntityTypeConfiguration Implementation

    }

}

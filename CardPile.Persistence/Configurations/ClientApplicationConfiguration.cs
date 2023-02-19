using CardPile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPile.Persistence.Configurations
{

    public class ClientApplicationConfiguration : IEntityTypeConfiguration<ClientApplication>
    {

        #region - - - - - - IEntityTypeConfiguration Implementation - - - - - -

        public void Configure(EntityTypeBuilder<ClientApplication> entity)
        {
            entity.ToTable("ClientApplication");

            // AccountID
            entity.HasKey(a => a.ClientApplicationID);
            entity.Property(a => a.ClientApplicationID)
                  .HasColumnName("ClientApplicationID")
                  .ValueGeneratedOnAdd();

            //Access Token
            entity.Property(e => e.AccessToken)
                .IsRequired(false)
                .HasMaxLength(500)
                .IsUnicode(false);

            // Name
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.HasIndex(e => e.Name)
                .HasDatabaseName("UQ_ClientApplication_Name")
                .IsUnique();

            // Secret
            entity.Property(e => e.Secret)
                .HasMaxLength(500)
                .IsRequired();
        }

        #endregion IEntityTypeConfiguration Implementation

    }

}

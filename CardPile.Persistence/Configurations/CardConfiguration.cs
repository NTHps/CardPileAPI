using CardPile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPile.Persistence.Configurations
{

    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {

        #region - - - - - - IEntityTypeConfiguration Implementation - - - - - -

        public void Configure(EntityTypeBuilder<Card> entity)
        {
            _ = entity.ToTable("Card");

            _ = entity.HasKey(e => e.CardID);
            _ = entity.Property(e => e.CardID)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired();
        }

        #endregion IEntityTypeConfiguration Implementation

    }

}

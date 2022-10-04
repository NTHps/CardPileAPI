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
            entity.ToTable("Card");

            entity.Property(e => e.CardID);

            entity.Property(e => e.CardName)
                .IsRequired();
        }

        #endregion IEntityTypeConfiguration Implementation

    }

}

using CardPile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPile.Persistence.Configurations
{

    public class DeckListConfiguration : IEntityTypeConfiguration<DeckList>
    {

        #region - - - - - - IEntityTypeConfiguration Implementation - - - - - -

        public void Configure(EntityTypeBuilder<DeckList> entity)
        {
            entity.ToTable("DeckList");

            entity.Property(e => e.DeckListID);

            entity.Property(e => e.DeckListName)
                .IsRequired();
        }

        #endregion IEntityTypeConfiguration Implementation

    }

}

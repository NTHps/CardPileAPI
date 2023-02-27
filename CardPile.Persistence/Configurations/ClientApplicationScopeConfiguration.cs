using CardPile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPile.Persistence.Configurations
{

    public class ClientApplicationScopeConfiguration : IEntityTypeConfiguration<ClientApplicationScope>
    {

        #region - - - - - - IEntityTypeConfiguration - - - - - -

        public void Configure(EntityTypeBuilder<ClientApplicationScope> entity)
        {
            entity.ToTable("ClientApplicationScope");

            entity.HasKey(e => new { e.ClientApplicationID, e.ScopeID });

            // ClientApplicationID
            entity.Property(e => e.ClientApplicationID).HasColumnName("ClientApplicationID");
            entity.HasOne(d => d.ClientApplication)
                .WithMany(p => p.ClientApplicationScope)
                .HasForeignKey(d => d.ClientApplicationID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientApplicationScope_ClientApplication");

            // ScopeID
            entity.Property(e => e.ScopeID).HasColumnName("ScopeID");
            entity.HasOne(d => d.Scope)
                .WithMany(p => p.ClientApplicationScopes)
                .HasForeignKey(d => d.ScopeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientApplicationScope_Scope");

        }

        #endregion IEntityTypeConfiguration

    }

}

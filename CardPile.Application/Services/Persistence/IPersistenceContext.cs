namespace CardPile.Application.Services.Persistence
{

    public interface IPersistenceContext
    {

        void Add<TEntity>(TEntity entity) where TEntity : class;

        TEntity Find<TEntity>(object[] keyValues) where TEntity : class;

        IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }

}

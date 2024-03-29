﻿using CardPile.Application.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CardPile.Persistence.Persistence
{

    public class PersistenceContext : DbContext, IPersistenceContext
    {

        #region - - - - - - Constructors - - - - - -

        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options) { }

        #endregion Constructors

        #region - - - - - - IPersistenceContext Implementation - - - - - -

        void IPersistenceContext.Add<TEntity>(TEntity entity)
            => this.Add(entity);

        TEntity IPersistenceContext.Find<TEntity>(object[] keyValues)
            => this.Find<TEntity>(keyValues);

        IQueryable<TEntity> IPersistenceContext.GetEntities<TEntity>() where TEntity : class
            => this.Set<TEntity>().AsQueryable();

        void IPersistenceContext.Remove<TEntity>(TEntity entity)
            => this.Remove(entity);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly);

        // Writing this here for future me: EntityFrameworkCore\Add-Migration CardPileDB -Context PersistenceContext
        // Add-Migration 'whatever'
        // Update-Databse || just run the api, we do it onm startup

        #endregion IPersistenceContext Implementation

    }

}

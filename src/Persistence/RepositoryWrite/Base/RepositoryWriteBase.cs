using Application.IRepositoryWrite.Base;
using Common;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.RepositoryWrite.Base
{
    public abstract class RepositoryWriteBase<T> : IRepositoryWriteBase<T>
       where T : class, IEntity
    {

        private readonly PersistanceDBContext Context;
        protected abstract string QueueName { get; }
        protected RepositoryWriteBase(PersistanceDBContext context)
        {
            Context = context;
        }

        protected IQueryable<T> GetAllAsQueryable()
        {
            IQueryable<T> query = from p in Context.Set<T>()
                                  select p;
            query = query.AsNoTracking();
            return query;
        }

        public virtual async Task DeleteItem(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Save();
        }

        public virtual async Task Insert(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Save();
        }
        public virtual async Task Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Save();
        }
        public async Task Save()
        {
            var entities = Context.ChangeTracker.Entries().Where(p => p.State != EntityState.Unchanged);
            foreach (var entity in entities)
            {
                FillEntityProperty(entity);
            }
            await Context.SaveChangesAsync(CancellationToken.None);

        }
        private void FillEntityProperty(EntityEntry entity)
        {
            if (entity.State == EntityState.Added)
            {
                SetID(entity);

            }

        }

        private void SetID(EntityEntry entity)
        {
            if (entity.Entity is IEntity item)
            {
                item.Id = (GetAllAsQueryable().Max(x => (int?)x.Id) ?? 0)+1;
            }
        }
        public async Task<T?> Find(int id)
        {
            return await GetAllAsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> Find(Expression<Func<T, bool>> expression)
        {
            return await GetAllAsQueryable().FirstOrDefaultAsync(expression);
        }

    }
}

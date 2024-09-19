using Domain;
using Domain.CaseAktifAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        public readonly DbSet<TEntity> _entities;
        protected readonly DbContext _dbContext;
        protected GenericRepository(DbContext context)
        {
            _entities = context.Set<TEntity>();
            _dbContext = context;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<TEntity>> AllAsync()
        {
            return await _entities.AsQueryable().ToListAsync();
        }

        public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<T> FindByAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancelationToken)
        {
            return await _entities.Where(predicate).Select(selector).SingleOrDefaultAsync(cancelationToken);
        }

        public async Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> FilterByAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken)
        {
            return await _entities.AsNoTracking().Where(predicate).Select(selector).ToListAsync(cancellationToken);
        }

        public async Task SaveAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
            var entityEntry = _entities.Update(entity);
            return entityEntry.Entity;
        }

        public async Task<bool> BulkUpdateAsync(List<TEntity> entities)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                _entities.UpdateRange(entities);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Hata : " + ex.Message);
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).CountAsync();
        }
        public async Task<int> CountAllAsync()
        {
            return await _entities.CountAsync();
        }
        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default)
            => query.FirstOrDefaultAsync(cancellationToken);

        public IQueryable<TEntity> Queryable()
        => _entities;

        public IQueryable<TEntity> Include<TProperty>(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> property)
        => query.Include(property);

        public async Task BulkAddAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                _entities.AddRange(entities);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Hata : " + ex.Message);
            }

        }
        public Task<List<T>> ToListAsync<T>(IQueryable<T> query)
            => query.ToListAsync();
    }

}

using Domain.CaseAktifAggregate;
using System.Linq.Expressions;

namespace Domain
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> AllAsync();
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T> FindByAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancelationToken);
        Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<T>> FilterByAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken);
        Task SaveAsync(TEntity entity);
        //to do update has to be async
        TEntity Update(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAllAsync();
        IQueryable<TEntity> Include<TProperty>(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> property);
        IQueryable<TEntity> Queryable();
        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);
        Task BulkAddAsync(IEnumerable<TEntity> entities);
        Task<List<T>> ToListAsync<T>(IQueryable<T> query);
        Task<bool> BulkUpdateAsync(List<TEntity> entities);
    }

}

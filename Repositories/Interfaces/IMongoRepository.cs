using ServiceCollectionAPI.Models;
using System.Linq.Expressions;

namespace ServiceCollectionAPI.Repositories.Interfaces
{
    public interface IMongoRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        IQueryable<TBaseEntity> AsQueryable();

        IEnumerable<TBaseEntity> FilterBy(
            Expression<Func<TBaseEntity, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TBaseEntity, bool>> filterExpression,
            Expression<Func<TBaseEntity, TProjected>> projectionExpression);
        Task<IList<TBaseEntity>> FilterByAsync(Expression<Func<TBaseEntity, bool>> filterExpression);

        Task<TBaseEntity> FindOneAsync(Expression<Func<TBaseEntity, bool>> filterExpression, bool tenantDisabled = false);

        Task<TBaseEntity> FindByIdAsync(string id, bool tenantDisabled = false);

        Task InsertOneAsync(TBaseEntity document, bool tenantDisabled = false);

        Task InsertManyAsync(ICollection<TBaseEntity> documents);

        Task ReplaceOneAsync(TBaseEntity document, bool tenantDisabled = false);

        Task DeleteOneAsync(Expression<Func<TBaseEntity, bool>> filterExpression);

        Task DeleteByIdAsync(string id);

        Task DeleteManyAsync(Expression<Func<TBaseEntity, bool>> filterExpression);
    }
}

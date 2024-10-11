using ServiceCollectionAPI.Attributes;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ServiceCollectionAPI.Repositories
{
    public class MongoRepository<TBaseEntity> : IMongoRepository<TBaseEntity>
        where TBaseEntity : BaseEntity
    {
        private readonly IConfiguration Configuration;

        private readonly IMongoCollection<TBaseEntity> _collection;
        private readonly ITenantContextService _tenantContextService;

        public MongoRepository(IConfiguration configuration, ITenantContextService tenantContextService)
        {
            Configuration = configuration;
            _tenantContextService = tenantContextService;

            var dbConnectionString = Configuration["ConnectionString"];
            var dbName = Configuration["DatabaseName"];

            if (string.IsNullOrEmpty(dbConnectionString) || string.IsNullOrEmpty(dbName))
            {
                throw new MissingConfiguration("No db settings");
            }

            try
            {
                var db = new MongoClient(dbConnectionString).GetDatabase(dbName);
                _collection = db.GetCollection<TBaseEntity>(GetCollectionName(typeof(TBaseEntity)));
            }
            catch (NoCollectionFoundException ex)
            {
                //aia e
            }

        }

        private protected string? GetCollectionName(Type documentType)
        {
            return (documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault() as BsonCollectionAttribute)?.CollectionName;
        }

        public virtual IQueryable<TBaseEntity> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TBaseEntity> FilterBy(Expression<Func<TBaseEntity, bool>> filterExpression)
        {
            var tenantId = _tenantContextService.GetTenantId();

            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filterExpression);

            return _collection.Find(combinedFilter).ToEnumerable();
        }

        public virtual async Task<IList<TBaseEntity>> FilterByAsync(Expression<Func<TBaseEntity, bool>> filterExpression)
        {
            var tenantId = _tenantContextService.GetTenantId();

            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filterExpression);

            return await _collection.Find(combinedFilter).ToListAsync();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TBaseEntity, bool>> filterExpression,
            Expression<Func<TBaseEntity, TProjected>> projectionExpression)
        {
            var tenantId = _tenantContextService.GetTenantId();

            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filterExpression);

            return _collection.Find(combinedFilter).Project(projectionExpression).ToEnumerable();
        }

        public virtual async Task<TBaseEntity> FindOneAsync(Expression<Func<TBaseEntity, bool>> filterExpression, bool tenantDisabled = false)
        {
            if (!tenantDisabled)
            {
                var tenantId = _tenantContextService.GetTenantId();

                var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
                var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filterExpression);

                return await _collection.Find(combinedFilter).FirstOrDefaultAsync();
            }
            else
            {
                return await _collection.Find(filterExpression).FirstOrDefaultAsync();
            }

        }

        public virtual async Task<TBaseEntity> FindByIdAsync(string id, bool tenantDisabled = false)
        {
            var tenantId = _tenantContextService.GetTenantId();
            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);

            if (string.IsNullOrEmpty(id) || id.Length != 24)
            {
                throw new InvalidIdException("Invalid id.");
            }

            var objectId = new ObjectId(id);

            var filter = Builders<TBaseEntity>.Filter.Eq(doc => doc.Id, objectId);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filter);

            return await _collection.Find(tenantDisabled ? filter : combinedFilter).SingleOrDefaultAsync();
        }

        public virtual async Task InsertOneAsync(TBaseEntity document, bool tenantDisabled = false)
        {
            if (!tenantDisabled)
            {
                var tenantId = _tenantContextService.GetTenantId();
                document.TenantId = new ObjectId(tenantId);
            }

            await _collection.InsertOneAsync(document);
        }

        public virtual async Task InsertManyAsync(ICollection<TBaseEntity> documents)
        {
            var tenantId = _tenantContextService.GetTenantId();
            foreach (var document in documents)
            {
                document.TenantId = new ObjectId(tenantId);
            }

            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task ReplaceOneAsync(TBaseEntity document, bool tenantDisabled = false)
        {
            var tenantId = _tenantContextService.GetTenantId();

            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
            var filter = Builders<TBaseEntity>.Filter.Eq(doc => doc.Id, document.Id);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filter);

            await _collection.FindOneAndReplaceAsync(tenantDisabled ? filter : combinedFilter, document);
        }

        public async Task DeleteOneAsync(Expression<Func<TBaseEntity, bool>> filterExpression)
        {
            var tenantId = _tenantContextService.GetTenantId();

            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filterExpression);

            await _collection.FindOneAndDeleteAsync(combinedFilter);
        }

        public async Task DeleteByIdAsync(string id)
        {
            var tenantId = _tenantContextService.GetTenantId();
            var objectId = new ObjectId(id);

            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
            var filter = Builders<TBaseEntity>.Filter.Eq(doc => doc.Id, objectId);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filter);

            await _collection.FindOneAndDeleteAsync(combinedFilter);
        }
        public async Task DeleteManyAsync(Expression<Func<TBaseEntity, bool>> filterExpression)
        {
            var tenantId = _tenantContextService.GetTenantId();

            var tenantFilter = Builders<TBaseEntity>.Filter.Eq("TenantId", tenantId);
            var combinedFilter = Builders<TBaseEntity>.Filter.And(tenantFilter, filterExpression);

            await _collection.DeleteManyAsync(combinedFilter);
        }
    }
}

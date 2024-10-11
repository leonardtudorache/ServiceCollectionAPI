using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceCollectionAPI.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;

        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        [BsonRepresentation(BsonType.String)]
        public ObjectId TenantId { get; set; }
    }
}

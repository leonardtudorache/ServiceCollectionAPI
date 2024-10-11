using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    [BsonCollection("Offer")]
    public class Offer : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<Package>? Packages { get; set; }
    }
}

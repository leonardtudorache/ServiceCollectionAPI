using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    //this attribute should be added when class name is not the same as collection name otherwise will be the same
    [BsonCollection("Products")]
    public class Product : BaseEntity
    {
        public int Quantity { get; set; }
        public string Name { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public decimal Price { get; set; }

    }
}
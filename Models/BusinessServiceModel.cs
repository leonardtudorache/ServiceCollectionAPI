using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    //this attribute should be added when class name is not the same as collection name otherwise will be the same
    [BsonCollection("Service")]
    public class BusinessServiceModel : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string ServiceType { get; set; } = null!;
        public string? Description { get; set; }
        public List<Employee>? Employees { get; set; } = new List<Employee>();

        //public virtual List<PackagePhoto> Photos { get; set; }
        //public virtual List<Product> Products { get; set; }
        //public virtual List<PackageOffer> PackageOffers { get; set; }
        //public virtual List<ClientOfferPackages> ClientOfferPackages { get; set; }
    }
}

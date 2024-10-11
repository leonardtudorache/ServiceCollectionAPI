using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    //this attribute should be added when class name is not the same as collection name otherwise will be the same
    [BsonCollection("Package")]
    public class Package : BaseEntity
    {
        public string Name { get; set; }=null!;
        public decimal Price { get; set; }
        public string PackageType { get; set; }=null!;
        public virtual List<Product>? Products { get; set; }
        public virtual List<BusinessServiceModel>? Services{get;set;}
    }
}

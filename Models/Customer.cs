using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    //this attribute should be added when class name is not the same as collection name otherwise will be the same
    [BsonCollection("Customer")]
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }=null!;
        public string LastName { get; set; }=null!;
        public string Email { get; set; }=null!;
        public string Phone { get; set; }=null!;

    }
}

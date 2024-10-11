using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    [BsonCollection("InvoiceItem")]
    public class InvoiceItem : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }

    }
}


using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    [BsonCollection("Invoice")]
    public class Invoice : BaseEntity
    {
        public int Number { get; set; }
        public string Serial { get; set; } = null!;
        public ClientOffer ClientOffer { get; set; } = null!;
        public PaymentType PaymentType { get; set; }
        public bool IsFiscal { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
    public enum PaymentType
    {
        Card = 0,
        Cash = 1
    }
    public enum InvoiceType
    {
        Invoice = 0,
        Receipt = 1
    }
}


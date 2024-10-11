using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models
{
    [BsonCollection("Contract")]
    public class Contract : BaseEntity
    {
        public Client Client { get; set; } = null!;
        public string SignatureUrl { get; set; } = "";
        public bool Signed { get; set; }
        public bool HasPrepayment { get; set; }
        public bool IsVoid { get; set; }
        public string Serial { get; set; } = null!;
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SignedOn { get; set; }
        public bool TermsAndConditions { get; set; }
        public ClientOffer? ClientOffer { get; set; }
    }
}


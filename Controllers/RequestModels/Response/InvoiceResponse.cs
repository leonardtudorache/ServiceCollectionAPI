using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Response
{
	public class InvoiceResponse
	{
        public string Id { get; set; } = "";
        public int Number { get; set; }
        public string Serial { get; set; } = null!;
        public ClientOffer ClientOffer { get; set; } = null!;
        public PaymentType PaymentType { get; set; }
        public bool IsFiscal { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}


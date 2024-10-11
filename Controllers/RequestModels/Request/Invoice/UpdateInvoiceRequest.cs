using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Request.Invoice
{
	public class UpdateInvoiceRequest
	{
        public int Number { get; set; }
        public string Serial { get; set; } = null!;
        public PaymentType PaymentType { get; set; }
        public bool IsFiscal { get; set; }
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}



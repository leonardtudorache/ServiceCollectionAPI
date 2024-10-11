namespace ServiceCollectionAPI.Controllers.RequestModels.Request.Contract
{
	public class CreateContractRequest
	{
        public Models.Client Client { get; set; } = null!;
        public string SignatureUrl { get; set; } = "";
        public bool Signed { get; set; }
        public bool HasPrepayment { get; set; }
        public bool IsVoid { get; set; }
        public string Serial { get; set; } = null!;
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public bool TermsAndConditions { get; set; }
        public string? ClientOfferId { get; set; }
    }
}


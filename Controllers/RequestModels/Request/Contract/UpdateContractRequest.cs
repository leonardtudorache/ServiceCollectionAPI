namespace ServiceCollectionAPI.Controllers.RequestModels.Request.Contract
{
	public class UpdateContractRequest
	{
        public string SignatureUrl { get; set; } = "";
        public bool Signed { get; set; }
        public bool HasPrepayment { get; set; }
        public bool IsVoid { get; set; }
    }
}


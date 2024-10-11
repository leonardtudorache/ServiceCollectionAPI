namespace ServiceCollectionAPI.Controllers.RequestModels.Generic
{
    public class Error
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }
    }
}

namespace ServiceCollectionAPI.Controllers.RequestModels.Generic
{
    public class Response<T> // single item
    {
        public T? Content { get; set; }

        //Don't know why it uses this because we have ActionResult
        public Error Error { get; set; }

        public bool IsSuccess => Error == null;

        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
    }
}

namespace ServiceCollectionAPI.Controllers.RequestModels.Generic
{
    public class PagedResponse<T> : Response<List<T>>
    {
        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }
    }
}

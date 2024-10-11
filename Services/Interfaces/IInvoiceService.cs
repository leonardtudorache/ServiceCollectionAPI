using ServiceCollectionAPI.Controllers.RequestModels.Request.Invoice;
using ServiceCollectionAPI.Controllers.RequestModels.Response;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceResponse>> GetAllInvoicesAsync();
        Task<InvoiceResponse> GetInvoiceByIdAsync(string id);
        Task CreateInvoiceAsync(CreateInvoiceRequest createRequest);
        Task UpdateInvoiceAsync(string id, UpdateInvoiceRequest updateRequest);
        Task DeleteInvoiceAsync(string id);
    }
}


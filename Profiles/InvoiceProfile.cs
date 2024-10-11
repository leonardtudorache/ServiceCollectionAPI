using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.EmployeeRequests;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Invoice;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateInvoiceRequest, Model.Invoice>();
            CreateMap<Model.Invoice, InvoiceResponse>();
            CreateMap<UpdateEmployeeRequest, Model.Invoice>();
            CreateMap<InvoiceResponse, Model.Invoice>();
        }
    }
}

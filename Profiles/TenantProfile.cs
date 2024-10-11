using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.TenantRequests;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateTenantRequest, Model.Tenant>();
            CreateMap<Model.Tenant, TenantResponse>();
        }
    }
}

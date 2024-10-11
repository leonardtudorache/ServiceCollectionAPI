using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ServiceRequest;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;

namespace ServiceCollectionAPI.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateServiceRequest, BusinessServiceModel>();
            CreateMap<UpdateServiceRequest, BusinessServiceModel>();
            CreateMap<BusinessServiceResponse, BusinessServiceModel>();
            CreateMap<BusinessServiceModel, BusinessServiceResponse>();
           
        }
    }
}

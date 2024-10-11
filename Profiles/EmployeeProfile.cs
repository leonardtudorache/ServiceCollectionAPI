using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.EmployeeRequests;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateEmployeeRequest, Model.Employee>();
            CreateMap<Model.Employee, EmployeeResponse>();
            CreateMap<UpdateEmployeeRequest, Model.Employee>();
            CreateMap<EmployeeResponse, Model.Employee>();
        }
    }
}

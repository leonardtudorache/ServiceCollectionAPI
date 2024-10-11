using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Model.Role, RoleResponse>();
        }
    }
}

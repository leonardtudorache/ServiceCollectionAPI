using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.UserRequests;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateUserRequest, Model.User>();
            CreateMap<Model.User, UserResponse>();
            CreateMap<UpdateUserRequest, Model.User>();
            
        }
    }
}

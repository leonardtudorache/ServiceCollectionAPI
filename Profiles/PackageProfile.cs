using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.PackageRquests;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class PackageProfile : Profile
    {
        public PackageProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreatePackageRequest, Model.Package>();
            CreateMap<Model.Package, PackageResponse>();
            CreateMap<UpdatePackageRequest, Model.Package>();
            CreateMap<PackageResponse, Model.Package>();
        }
    }
}


using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateProductRequest, Model.Product>();
            CreateMap<Model.Product, ProductResponse>();
            CreateMap<UpdateProductRequest, Model.Product>();
            CreateMap<ProductResponse, Model.Product>();
        }
    }
}

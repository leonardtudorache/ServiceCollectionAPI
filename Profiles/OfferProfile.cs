using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Offer;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using Model = ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateOfferRequest, Model.Offer>();
            CreateMap<Model.Offer, OfferResponse>();
            CreateMap<UpdateOfferRequest, Model.Offer>();
            CreateMap<OfferResponse, Model.Offer>();
        }
    }
}


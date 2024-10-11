using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.ClientOffer;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class ClientOfferProfile : Profile
    {
        public ClientOfferProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateClientOfferRequest, ClientOffer>();
            CreateMap<ClientOffer, ClientOfferResponse>();
            CreateMap<UpdateClientOfferRequest, ClientOffer>();
            CreateMap<ClientOfferResponse, ClientOffer>();
        }
    }
}


using ServiceCollectionAPI.Controllers.RequestModels.Request.Client;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Models;
using AutoMapper;

namespace ServiceCollectionAPI.Profiles
{
	public class ClientProfile: Profile
	{
        public ClientProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateClientRequest, Client>();
            CreateMap<Client, ClientResponse>();
            CreateMap<UpdateClientRequest, Client>();
            CreateMap<ClientResponse, Client>();
        }
    }
}


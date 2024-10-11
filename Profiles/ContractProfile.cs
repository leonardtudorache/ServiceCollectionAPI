using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Contract;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Profiles
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CreateContractRequest, Contract>();
            CreateMap<Contract, ContractResponse>();
            CreateMap<UpdateContractRequest, Contract>();
            CreateMap<ContractResponse, Contract>();
        }
    }
}


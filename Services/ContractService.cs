using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Contract;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class ContractService : IContractService
    {
        private readonly IMongoRepository<Contract> _contractRepository;
        private readonly IMongoRepository<ClientOffer> _clientOfferRepository;
        private readonly IMapper _mapper;

        public ContractService(IMongoRepository<Contract> contractRepository, IMongoRepository<ClientOffer> clientOfferRepository, IMapper mapper)
        {
            _contractRepository = contractRepository;
            _clientOfferRepository = clientOfferRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContractResponse>> GetAllContractsAsync()
        {
            var contracts = await _contractRepository.FilterByAsync(c => true);
            return _mapper.Map<IEnumerable<ContractResponse>>(contracts);
        }

        public async Task<ContractResponse> GetContractByIdAsync(string id)
        {
            var contract = await _contractRepository.FindByIdAsync(id);
            return _mapper.Map<ContractResponse>(contract);
        }

        public async Task CreateContractAsync(CreateContractRequest createRequest)
        {
            var contract = _mapper.Map<Contract>(createRequest);

            if (!string.IsNullOrEmpty(createRequest.ClientOfferId))
            {
                var clientOffer = await _clientOfferRepository.FindByIdAsync(createRequest.ClientOfferId);

                contract.ClientOffer = clientOffer;
            }

            await _contractRepository.InsertOneAsync(contract);
        }

        public async Task UpdateContractAsync(string id, UpdateContractRequest updateRequest)
        {
            var existingContract = await _contractRepository.FindByIdAsync(id);
            if (existingContract == null)
            {
                throw new ContractNotFoundException("Contract not found");
            }

            existingContract.SignatureUrl = updateRequest.SignatureUrl;
            existingContract.Signed = updateRequest.Signed;
            existingContract.HasPrepayment = updateRequest.HasPrepayment;
            existingContract.IsVoid = updateRequest.IsVoid;

            await _contractRepository.ReplaceOneAsync(existingContract);
        }

        public async Task DeleteContractAsync(string id)
        {
            await _contractRepository.DeleteByIdAsync(id);
        }
    }
}


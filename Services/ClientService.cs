using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Client;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IMongoRepository<Client> _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IMongoRepository<Client> clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientResponse>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.FilterByAsync(c => true);
            return _mapper.Map<IEnumerable<ClientResponse>>(clients);
        }

        public async Task<ClientResponse> GetClientByIdAsync(string id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            return _mapper.Map<ClientResponse>(client);
        }

        public async Task CreateClientAsync(CreateClientRequest createRequest)
        {
            var client = _mapper.Map<Client>(createRequest);
            await _clientRepository.InsertOneAsync(client);
        }

        public async Task UpdateClientAsync(string id, UpdateClientRequest updateRequest)
        {
            var existingClient = await _clientRepository.FindByIdAsync(id);
            if (existingClient == null)
            {
                throw new ClientNotFoundException("Client not found");
            }

            _mapper.Map(updateRequest, existingClient);
            await _clientRepository.ReplaceOneAsync(existingClient);
        }

        public async Task DeleteClientAsync(string id)
        {
            await _clientRepository.DeleteByIdAsync(id);
        }
    }
}


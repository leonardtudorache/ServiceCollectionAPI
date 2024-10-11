using AutoMapper;
using Model = ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.TenantRequests;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;

namespace ServiceCollectionAPI.Services
{
    public class TenantService : ITenantService
    {
        private readonly IMongoRepository<Model.Tenant> _tenantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantService> _logger;
        private readonly ITenantContextService _tenantContextService;

        public TenantService(IMongoRepository<Model.Tenant> tenantRepo, IMapper mapper, ILogger<TenantService> logger, ITenantContextService tenantContextService)
        {
            _tenantRepository = tenantRepo;
            _mapper = mapper;
            _logger = logger;
            _tenantContextService = tenantContextService;
        }

        public async Task<Tenant> InsertOneAsync(CreateTenantRequest createTenantRequest)
        {
            var tenantToInsert = _mapper.Map<Model.Tenant>(createTenantRequest);

            await _tenantRepository.InsertOneAsync(tenantToInsert);

            return tenantToInsert;
        }

        public async Task<Tenant> InsertEmptyTenant()
        {
            var tenantToInsert = new Tenant();

            await _tenantRepository.InsertOneAsync(tenantToInsert, true);

            return tenantToInsert;
        }

        public async Task<Tenant> GetTenant(string tenantId)
        {
            var tenant = await _tenantRepository.FindByIdAsync(tenantId, true);

            if (tenant == null)
            {
                throw new TenantNotFoundException("Tenant not found.");
            }

            return tenant;
        }

        public async Task<TenantResponse> GetTenantJson(string tenantId)
        {
            var tenant = await _tenantRepository.FindByIdAsync(tenantId, true);

            if (tenant == null)
            {
                throw new TenantNotFoundException("Tenant not found.");
            }

            return _mapper.Map<TenantResponse>(tenant);
        }

        public async Task UpdateTenant(EditTenantRequest editTenantRequest)
        {
            var tenantToEdit = await GetTenant(_tenantContextService.GetTenantId());

            tenantToEdit.Name = editTenantRequest.Name;
            tenantToEdit.Address = editTenantRequest.Address;
            tenantToEdit.City = editTenantRequest.City;
            tenantToEdit.County = editTenantRequest.County;
            tenantToEdit.CUI = editTenantRequest.CUI;
            tenantToEdit.RegComert = editTenantRequest.RegComert;
            tenantToEdit.IBAN = editTenantRequest.IBAN ?? "";
            tenantToEdit.Phone = editTenantRequest.Phone ?? "";

            await _tenantRepository.ReplaceOneAsync(tenantToEdit, true);
        }
    }
}

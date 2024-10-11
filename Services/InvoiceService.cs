using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Invoice;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMongoRepository<Invoice> _invoiceRepository;
        private readonly IMongoRepository<ClientOffer> _clientOfferRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IMongoRepository<Invoice> invoiceRepository, IMongoRepository<ClientOffer> clientOfferRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _clientOfferRepository = clientOfferRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceResponse>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.FilterByAsync(i => true);
            return _mapper.Map<IEnumerable<InvoiceResponse>>(invoices);
        }

        public async Task<InvoiceResponse> GetInvoiceByIdAsync(string id)
        {
            var invoice = await _invoiceRepository.FindByIdAsync(id);
            return _mapper.Map<InvoiceResponse>(invoice);
        }

        public async Task CreateInvoiceAsync(CreateInvoiceRequest createRequest)
        {
            var clientOffer = await _clientOfferRepository.FindByIdAsync(createRequest.ClientOfferId);
            if (clientOffer == null)
            {
                throw new ClientOfferNotFoundException("ClientOffer not found");
            }

            var invoice = new Invoice
            {
                Number = createRequest.Number,
                Serial = createRequest.Serial,
                ClientOffer = clientOffer,
                PaymentType = createRequest.PaymentType,
                IsFiscal = createRequest.IsFiscal,
                CreatedOn = createRequest.CreatedOn
            };

            await _invoiceRepository.InsertOneAsync(invoice);
        }

        public async Task UpdateInvoiceAsync(string id, UpdateInvoiceRequest updateRequest)
        {
            var existingInvoice = await _invoiceRepository.FindByIdAsync(id);
            if (existingInvoice == null)
            {
                throw new InvoiceNotFoundException("Invoice not found");
            }

            existingInvoice.Number = updateRequest.Number;
            existingInvoice.Serial = updateRequest.Serial;
            existingInvoice.PaymentType = updateRequest.PaymentType;
            existingInvoice.IsFiscal = updateRequest.IsFiscal;

            // Update the invoice items
            existingInvoice.Items = _mapper.Map<List<InvoiceItem>>(updateRequest.Items);

            await _invoiceRepository.ReplaceOneAsync(existingInvoice);
        }

        public async Task DeleteInvoiceAsync(string id)
        {
            await _invoiceRepository.DeleteByIdAsync(id);
        }
    }
}


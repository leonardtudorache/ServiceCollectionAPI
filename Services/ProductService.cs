using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;

namespace ServiceCollectionAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IMongoRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        public async Task AddProduct(CreateProductRequest createProductRequest)
        {
            Product productToCreate = _mapper.Map<Product>(createProductRequest);
            await _productRepository.InsertOneAsync(productToCreate);
        }

        public async Task<ProductResponse> GetProductById(string productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);

            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var products = await _productRepository.FilterByAsync(p => true);

            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task UpdateProduct(UpdateProductRequest updateProductRequest)
        {
            var product = _mapper.Map<Product>(updateProductRequest);

            var productToUpdate = await _productRepository.FindOneAsync(p => p.Id == product.Id);

            if (productToUpdate == null)
                throw new ArgumentException("Product not found");

            productToUpdate.Name = product.Name; ;
            productToUpdate.Price = product.Price;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.ProductType = product.ProductType;

            await _productRepository.ReplaceOneAsync(productToUpdate);


        }

        public async Task DeleteProduct(string productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);

            if (product == null)
                throw new ArgumentException("Product not found");

            await _productRepository.DeleteByIdAsync(productId);

        }


    }
}
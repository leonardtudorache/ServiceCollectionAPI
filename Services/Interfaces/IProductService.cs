using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;

public interface IProductService
{
    Task AddProduct(CreateProductRequest product);
    Task<ProductResponse> GetProductById(string id);
    Task<List<ProductResponse>> GetAllProducts();
    Task UpdateProduct(UpdateProductRequest product);
    Task DeleteProduct(string id);


}
namespace ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest
{
    public class CreateProductRequest
    {
        public int Quantity { get; set; }
        public string Name { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
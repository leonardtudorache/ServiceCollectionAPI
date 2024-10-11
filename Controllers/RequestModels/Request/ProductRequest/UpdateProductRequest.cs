namespace ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest
{
    public class UpdateProductRequest
    {
        public string Id { get; set; } = null!;
        public int Quantity { get; set; }
        public string Name { get; set; } = null!;
        public string ProductType { get; set; }=null!;
        public decimal Price { get; set; }
    }
}
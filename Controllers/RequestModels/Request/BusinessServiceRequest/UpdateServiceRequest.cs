using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest
{
    public class UpdateServiceRequest
    {
        public string Name { get; set; }=null!;
        public decimal Price { get; set; }
        public string ServiceType { get; set; }=null!;
        public string? Description { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Generic
{
    public class BusinessServiceResponse
    {
        public string Id { get; set; } ="";
        public string Name { get; set; }="";
        public decimal Price { get; set; }
        public string ServiceType { get; set; }="";
        public string? Description { get; set; }
        public List<Employee>? Employees { get; set; } = new List<Employee>();

    }
}

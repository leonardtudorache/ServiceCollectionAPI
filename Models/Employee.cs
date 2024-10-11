using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models;

[BsonCollection("Employee")]
public class Employee : User
{
    public string? Description { get; set; }
    public List<string> Links { get; set; } = new List<string>();
    public List<string> Photos { get; set; } = new List<string>();
}
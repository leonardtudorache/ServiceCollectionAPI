using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models;

[BsonCollection("Client")]
public class Client : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Cnp { get; set; } = null!;
    public string Address { get; set; } = null!;
    public ClientType Type { get; set; }

}

public enum ClientType
{
    Individual = 0,
    Company = 1
}
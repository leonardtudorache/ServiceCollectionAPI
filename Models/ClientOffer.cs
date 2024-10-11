using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models;

[BsonCollection("ClientOffer")]
public class ClientOffer : BaseEntity
{
    public Status Status { get; set; }
    public Client Client { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
}

public enum Status
{
    Pending = 0,
    Requested = 1,
    Confirmed = 2
}
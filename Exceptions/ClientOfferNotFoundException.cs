namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class ClientOfferNotFoundException : Exception
    {
        public ClientOfferNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

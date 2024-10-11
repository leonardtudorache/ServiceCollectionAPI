namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class OfferNotFoundException : Exception
    {
        public OfferNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

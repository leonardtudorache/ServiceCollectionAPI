namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

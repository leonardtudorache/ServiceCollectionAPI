namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class BusinessServiceNotFoundException : Exception
    {
        public BusinessServiceNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

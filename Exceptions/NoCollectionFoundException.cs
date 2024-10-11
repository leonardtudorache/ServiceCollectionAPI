namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class NoCollectionFoundException: Exception
    {
        public NoCollectionFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

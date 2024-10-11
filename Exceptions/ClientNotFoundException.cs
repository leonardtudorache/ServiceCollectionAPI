namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

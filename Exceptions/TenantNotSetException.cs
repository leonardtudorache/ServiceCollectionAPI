namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class TenantNotSetException : Exception
    {
        public TenantNotSetException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

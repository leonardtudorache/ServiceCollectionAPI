namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class TenantIdNotSetException : Exception
    {
        public TenantIdNotSetException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

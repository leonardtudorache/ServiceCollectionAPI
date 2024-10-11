namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class TenantNotFoundException : Exception
    {
        public TenantNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

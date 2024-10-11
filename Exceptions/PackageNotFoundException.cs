namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

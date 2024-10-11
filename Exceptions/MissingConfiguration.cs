namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class MissingConfiguration : Exception
    {
        public MissingConfiguration(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class ContractNotFoundException : Exception
    {
        public ContractNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

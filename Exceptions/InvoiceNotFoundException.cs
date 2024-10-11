namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class InvoiceNotFoundException : Exception
    {
        public InvoiceNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

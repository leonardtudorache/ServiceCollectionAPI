namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

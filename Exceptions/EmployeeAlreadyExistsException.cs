namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class EmployeeAlreadyExistsException : Exception
    {
        public EmployeeAlreadyExistsException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

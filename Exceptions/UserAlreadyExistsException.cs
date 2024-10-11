namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

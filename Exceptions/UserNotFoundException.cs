namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

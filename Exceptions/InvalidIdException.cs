namespace ServiceCollectionAPI.Exceptions
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

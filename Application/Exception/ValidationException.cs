namespace Application.Exception
{
    using System;

    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException() : this("Validation error occured")
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(Exception ex)
            : this(ex.Message)
        {
        }

    }
}

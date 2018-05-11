using System;

namespace CarParking.Exceptions
{
    public class NotEnoughSpaceException : Exception
    {
        public NotEnoughSpaceException()
        {
        }

        public NotEnoughSpaceException(string message)
            : base(message)
        {
        }

        public NotEnoughSpaceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
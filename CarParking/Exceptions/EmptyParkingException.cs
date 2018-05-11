using System;

namespace CarParking.Exceptions
{
    public class EmptyParkingException : Exception
    {
        public EmptyParkingException()
        {
        }

        public EmptyParkingException(string message)
            : base(message)
        {
        }

        public EmptyParkingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
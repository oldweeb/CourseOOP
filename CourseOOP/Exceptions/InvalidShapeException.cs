using System;

namespace CourseOOP.Exceptions
{
    public class InvalidShapeException : Exception
    {
        public InvalidShapeException() : base() { }

        public InvalidShapeException(string message) : base(message) {}
        public InvalidShapeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

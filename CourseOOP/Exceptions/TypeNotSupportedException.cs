using System;

namespace CourseOOP.Exceptions
{
    public class TypeNotSupportedException : Exception
    {
        public TypeNotSupportedException() : base() { }

        public TypeNotSupportedException(string message) : base(message) { }

        public TypeNotSupportedException(string message, Exception innerException) : base(message, innerException) { }
    }
}

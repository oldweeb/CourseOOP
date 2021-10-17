using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseOOP.Exceptions
{
    public class ShapeParseException : Exception
    {
        public ShapeParseException() : base() { }

        public ShapeParseException(string message) : base(message) { }

        public ShapeParseException(string message, Exception innerException) : base(message, innerException) { }
    }
}

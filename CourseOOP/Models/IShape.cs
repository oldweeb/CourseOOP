using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseOOP.Models
{
    public interface IShape
    {
        string ShapeType { get; }
        double GetArea();
        double GetPerimeter();
    }
}

namespace CourseOOP.Models
{
    public interface IShape
    {
        string ShapeType { get; }
        double GetArea();
        double GetPerimeter();
    }
}

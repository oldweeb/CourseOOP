using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using CourseOOP.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CourseOOP.Models
{
    public class ShapeHandler
    {
        public List<IShape> Shapes { get; }
        public Dictionary<IShape, StringBuilder> ShapesHistory { get; }

        public IShape this[int index]
        {
            get
            {
                if (index < 0 || index >= Shapes.Count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

                return Shapes[index];
            }
        }

        public ShapeHandler()
        {
            Shapes = new List<IShape>();
            ShapesHistory = new Dictionary<IShape, StringBuilder>();
        }

        public ShapeHandler(string filePath)
        {
            Shapes = new List<IShape>();
            ShapesHistory = new Dictionary<IShape, StringBuilder>();
            ReadFromFile(filePath);
        }

        public ShapeHandler(ShapeHandler handler)
        {
            Shapes = handler.Shapes.ToList();
            ShapesHistory = handler.ShapesHistory.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        public void ReadFromFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("String is null or whitespace.", nameof(filePath));
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            FileInfo fileInfo = new(filePath);
            if (String.CompareOrdinal(fileInfo.Extension, ".txt") != 0 &&
                String.CompareOrdinal(fileInfo.Extension, ".json") != 0)
            {
                throw new NotSupportedException("Only *.txt and *.json files are supported.");
            }

            string file;
            using (StreamReader reader = new(filePath))
            {
                file = reader.ReadToEnd();
            }
            if (String.CompareOrdinal(fileInfo.Extension, ".json") == 0)
            {
                ReadJson(file);
                return;
            }
            using (StreamReader reader = new(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    line = line.Trim();
                    string shapeType = line.Substring(0, line.IndexOf(' '));
                    IShape shape;
                    switch (shapeType)
                    {
                        case "Triangle":
                            shape = Triangle.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        case "RightTriangle":
                            shape = RightTriangle.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        case "IsoscelesTriangle":
                            shape = IsoscelesTriangle.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        case "EquilateralTriangle":
                            shape = EquilateralTriangle.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        case "Quadrangle":
                            shape = Quadrangle.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        case "Rectangle":
                            shape = Rectangle.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        case "Trapezium":
                            shape = Rectangle.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        case "Hexagon":
                            shape = Hexagon.Parse(line.Remove(0, shapeType.Length + 1));
                            break;
                        default:
                            throw new TypeNotSupportedException("This type of shapes is not supported.");
                    }
                    Shapes.Add(shape);
                    if (!ShapesHistory.ContainsKey(shape))
                    {
                        ShapesHistory.Add(shape, new StringBuilder($"{DateTime.Now} - shape was created.\n"));
                    }
                    else
                    {
                        ShapesHistory[shape].AppendLine($"{DateTime.Now} - shape was created.");
                    }
                }
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Shapes.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            IShape shape = Shapes.ElementAt(index);
            Shapes.RemoveAt(index);
            ShapesHistory[shape].AppendLine($"{DateTime.Now} - shape was removed.");
        }
        public void ReadJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken shapesJToken = jObject["Shapes"] ?? throw new FormatException("Json file must contain attribute \"Shapes\"");
            foreach (JToken shapeJToken in shapesJToken)
            {
                JToken typeJT = shapeJToken["Type"] ?? throw new FormatException("Each token must contain attribute \"Type\".");
                string type = typeJT.Value<string>();
                IShape shape;
                switch (type)
                {
                    case "Triangle":
                        shape = shapeJToken.ToObject<Triangle>();
                        Triangle triangle = (Triangle)shape;
                        if (!Triangle.IsTriangle(triangle.A, triangle.B, triangle.C))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "IsoscelesTriangle":
                        shape = shapeJToken.ToObject<IsoscelesTriangle>();
                        IsoscelesTriangle isosceles = (IsoscelesTriangle) shape;
                        if (!IsoscelesTriangle.IsIsoscelesTriangle(
                            isosceles.A,
                            isosceles.B,
                            isosceles.C
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "EquilateralTriangle":
                        shape = shapeJToken.ToObject<EquilateralTriangle>();
                        EquilateralTriangle equilateral = (EquilateralTriangle) shape;
                        if (!EquilateralTriangle.IsEquilateralTriangle(
                            equilateral.A,
                            equilateral.B,
                            equilateral.C
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "RightTriangle":
                        shape = shapeJToken.ToObject<RightTriangle>();
                        RightTriangle right = (RightTriangle) shape;
                        if (!RightTriangle.IsRightTriangle(right.A, right.B, right.C))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Quadrangle":
                        shape = shapeJToken.ToObject<Quadrangle>();
                        Quadrangle quadrangle = (Quadrangle) shape;
                        if (!Quadrangle.IsQuadrangle(
                            quadrangle.A,
                            quadrangle.B,
                            quadrangle.C,
                            quadrangle.D
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Rectangle":
                        shape = shapeJToken.ToObject<Rectangle>();
                        Rectangle rectangle = (Rectangle) shape;
                        if (!Rectangle.IsRectangle(
                            rectangle.A,
                            rectangle.B,
                            rectangle.C,
                            rectangle.D
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Trapezium":
                        shape = shapeJToken.ToObject<Trapezium>();
                        Trapezium trapezium = (Trapezium) shape;
                        if (!Trapezium.IsTrapezium(
                            trapezium.A,
                            trapezium.B,
                            trapezium.C,
                            trapezium.D
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Hexagon":
                        shape = shapeJToken.ToObject<Hexagon>();
                        break;
                    default:
                        throw new TypeNotSupportedException("This type of shapes is not supported.");
                }

                Shapes.Add(shape ?? throw new ShapeParseException("Failed to parse shape from JSON."));
                if (!ShapesHistory.ContainsKey(shape))
                {
                    ShapesHistory.Add(shape, new StringBuilder($"{DateTime.Now} - shape was created.\n"));
                }
                else
                {
                    ShapesHistory[shape].AppendLine($"{DateTime.Now} - shape was created.");
                }
            }
        }

        public void AddShape([DisallowNull] IShape shape)
        {
            if (shape == null)
            {
                throw new ArgumentNullException(nameof(shape), "Parameter is null.");
            }
            Shapes.Add(shape);
            if (!ShapesHistory.ContainsKey(shape))
            {
                ShapesHistory.Add(shape, new StringBuilder($"{DateTime.Now} - shape was created.\n"));
            }
            else
            {
                _ = ShapesHistory[shape].AppendLine($"{DateTime.Now} - shape was created.");
            }
        }

        public void UpdateShapeAt([DisallowNull] IShape shape, int index)
        {
            if (shape == null)
            {
                throw new ArgumentNullException(nameof(shape), "Argument is null.");
            }

            if (index < 0 || index >= Shapes.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            StringBuilder data = ShapesHistory[shape];
            ShapesHistory.Remove(shape);
            Shapes[index] = shape;
            ShapesHistory.Add(shape, data);
            _ = data.AppendLine($"{DateTime.Now} - shape was edited.");
        }
        public void WriteToFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("String is null or whitespace.", nameof(filePath));
            }

            FileInfo fileInfo = new(filePath);
            if (String.CompareOrdinal(fileInfo.Extension, ".txt") != 0 &&
                String.CompareOrdinal(fileInfo.Extension, ".json") != 0)
            {
                throw new NotSupportedException("Only *.txt and *.json files are supported.");
            }
            if (String.CompareOrdinal(fileInfo.Extension, ".json") == 0)
            {
                using (StreamWriter writer = new(filePath))
                {
                    writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
                }
            }
            else
            {
                using (StreamWriter writer = new(filePath))
                {
                    foreach (IShape shape in this)
                    {
                        writer.WriteLine(shape.ToString());
                    }
                }
            }
        }

        public void WriteShapesHistoryToFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("String is null or whitespace.", nameof(filePath));
            }

            FileInfo fileInfo = new(filePath);
            if (String.CompareOrdinal(fileInfo.Extension, ".txt") != 0)
            {
                throw new NotSupportedException("Only *.txt files are supported.");
            }

            using StreamWriter writer = new(filePath);
            foreach ((IShape shape, StringBuilder history) in ShapesHistory)
            {
                writer.WriteLine($"Shape: {shape.ShapeType} Information: {shape}\n");
                writer.WriteLine($"History: {history}\n");
            }

        }
        public IEnumerator<IShape> GetEnumerator() => Shapes.GetEnumerator();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseOOP.Models
{
    public class ShapeHandler
    {
        private List<IShape> _shapes;

        public IShape this[int index]
        {
            get
            {
                if (index < 0 || index >= _shapes.Count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

                return _shapes[index];
            }
        }

        public ShapeHandler()
        {
            _shapes = new List<IShape>();
        }

        public ShapeHandler(string filePath)
        {
            _shapes = new List<IShape>();
        }

        public ShapeHandler(ShapeHandler handler)
        {
            _shapes = handler._shapes.ToList();
        }

        public void ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            FileInfo fileInfo = new(filePath);
            if (String.CompareOrdinal(fileInfo.Extension, ".txt") != 0 ||
                String.CompareOrdinal(fileInfo.Extension, ".json") != 0)
            {
                throw new NotSupportedException("Only *.txt and *.json files are supported.");
            }

            /*string[] lines = null;
            using (StreamReader reader = new(filePath))
            {
                lines = reader.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
            }*/
            throw new NotImplementedException();

        }
    }
}

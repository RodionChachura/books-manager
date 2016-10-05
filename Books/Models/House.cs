using Books.Abstraction;
using System.Collections.Generic;

namespace Books.Models
{
    public class House : IModel
    {
        public House()
        {
            Books = new List<string>();
        }

        public string Name { get; set; }
        public string City { get; set; }
        public List<string> Books { get; set; }
    }
}

using Books.Abstraction;
using System.Collections.Generic;

namespace Books.Models
{
    public class PublicationHouse : IModel
    {
        public PublicationHouse()
        {
            Books = new List<Book>();
        }
        public string Name { get; set; }
        public string City { get; set; }
        public List<Book> Books { get; private set; }
    }
}

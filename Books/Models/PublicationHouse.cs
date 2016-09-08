using Books.Abstraction;
using System.Collections.Generic;

namespace BooksCardFile.Models
{
    public class PublicationHouse : IModel
    {
        public PublicationHouse()
        {
            Books = new List<Book>();
        }
        public string Name { get; set; }
        public string Sity { get; set; }
        public List<Book> Books { get; private set; }
    }
}

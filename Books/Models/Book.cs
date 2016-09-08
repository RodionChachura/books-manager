using Books;
using Books.Abstraction;
using System.Collections.Generic;

namespace BooksCardFile.Models
{
    public class Book : IModel
    {
        public Book()
        {
            Authors = new List<Author>();
            Tags = new List<Tags>();
        }
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public List<Tags> Tags { get; set; }
        public int PublicationYear { get; set; }
        public PublicationHouse PublicationHouse { get; set; }
    }
}

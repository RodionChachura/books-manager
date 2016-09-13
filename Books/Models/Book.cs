using Books.Abstraction;
using System.Collections.Generic;

namespace Books.Models
{
    public class Book : IModel
    {
        public Book()
        {
            Authors = new List<Author>();
            Tags = new List<string>();
        }
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public List<string> Tags { get; set; }
        public int PublicationYear { get; set; }
        public House House { get; set; }
    }
}

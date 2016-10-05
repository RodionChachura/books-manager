using Books.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Books.Collections
{
    public class AuthorsCollection : ObservableDictionary<string, Author>
    {
        public AuthorsCollection() : base()
        {
        }
        public void Add(Author author)
        {
            Add(author.Name, author);
        }
        public List<Author> ToList()
        {
            return Values.Cast<Author>().ToList();
        }
    }
}

using Books.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

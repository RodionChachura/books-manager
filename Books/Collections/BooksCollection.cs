using Books.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Collections
{
    public class BooksCollection : ObservableDictionary<string, Book>
    {
        public BooksCollection() : base()
        {
        }
        public void Add(Book book)
        {
            Add(book.Name, book);
        }
        public List<Book> ToList()
        {
            return Values.Cast<Book>().ToList();
        }
    }
}

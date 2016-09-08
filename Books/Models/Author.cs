using Books.Abstraction;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace BooksCardFile.Models
{
    public class Author : IModel
    {
        public Author()
        {
            Books = new List<Book>();
        }
        public string Name { get; set; }
        public DateTime DayOfBirdth { get; set; }
        public Image Photo { get; set; }
        public List <Book> Books { get; private set; }
    }
}

using Books.Abstraction;
using System;
using System.Collections.Generic;

namespace Books.Models
{
    public class Author : IModel

    {
        public Author()
        {
            Books = new List<string>();
        }

        public string Name { get; set; }
        public DateTime DayOfBirdth { get; set; }
        public string Photo { get; set; }
        public List <string> Books { get; set; }
    }
}

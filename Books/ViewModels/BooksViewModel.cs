using Books.Abstraction;
using Books.Collections;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Books.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.ViewModels
{
    public class BooksViewModel : ViewModelsAndModelsCollection<BookViewModel, Book>
    {
        public BooksViewModel(BooksCollection models) : base(models)
        {
        }
    }

}

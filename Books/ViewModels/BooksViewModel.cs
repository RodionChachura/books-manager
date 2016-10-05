using Books.Collections;
using Books.Models;

namespace Books.ViewModels
{
    public class BooksViewModel : ViewModelsAndModelsCollection<BookViewModel, Book>
    {
        public BooksViewModel(BooksCollection models) : base(models)
        {
        }
    }

}

using Books.Collections;
using Books.Models;

namespace Books.ViewModels
{
    public class AuthorsViewModel : ViewModelsAndModelsCollection<AuthorViewModel, Author>
    {
        public AuthorsViewModel(AuthorsCollection models) : base(models)
        {
        }
    }

}

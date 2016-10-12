using Books.Commands;
using System.Windows.Input;

namespace Books.ViewModels
{
    public class MainViewModel
    {
        public BooksViewModel BooksViewModel { get; private set; }
        public AuthorsViewModel AuthorsViewModel { get; private set; }
        public HousesViewModel HousesViewModel { get; private set; }

        public MainViewModel()
        {
            BooksViewModel = new BooksViewModel(DataManager.Books);
            AuthorsViewModel = new AuthorsViewModel(DataManager.Authors);
            HousesViewModel = new HousesViewModel(DataManager.Houses);
        }

        public ICommand RestoreCommand
        {
            get
            {
                return new RestoreCommand();
            }
        }
        public ICommand languageCommand
        {
            get
            {
                return new languageCommand();
            }
        }
    }
}

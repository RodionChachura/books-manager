using System.Windows.Input;

namespace Books.Abstraction
{
    public interface IViewModel
    {
        IModel Model { get; set; }
        ICommand SaveCommand { get;}
        ICommand RemoveCommand { get;}
        string Name { get; set; }
    }
}

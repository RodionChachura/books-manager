using System;
using System.Windows.Input;

namespace Books.Commands
{
    public class RemoveBookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        string name;
        public RemoveBookCommand(string name)
        {
            this.name = name;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataManager.RemoveBook(name);
        }
    }
}

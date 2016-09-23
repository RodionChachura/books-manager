using System;
using System.Windows.Input;

namespace Books.Commands
{
    public class RemoveAuthorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        string name;
        public RemoveAuthorCommand(string name)
        {
            this.name = name;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataManager.RemoveAuthor(name);
        }
    }
}

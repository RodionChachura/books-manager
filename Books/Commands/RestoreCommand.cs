using System;
using System.Windows.Input;

namespace Books.Commands
{
    class RestoreCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataManager.RestoreData();
        }
    }
}

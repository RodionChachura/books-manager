using System;
using System.Windows.Input;

namespace Books.Commands
{
    public class RemoveHouseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        string name;
        public RemoveHouseCommand(string name)
        {
            this.name = name;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataManager.RemoveHouse(name);
        }
    }
}

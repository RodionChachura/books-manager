using Books.Models;
using Books.ViewModels;
using System;
using System.Windows.Input;

namespace Books.Commands
{
    public class SaveHouseCommand : ICommand
    {
        House model;
        public SaveHouseCommand(House model)
        {
            this.model = model;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataManager.AddHouse(model);
        }
    }
}

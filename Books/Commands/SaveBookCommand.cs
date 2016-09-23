using Books.Models;
using Books.ViewModels;
using System;
using System.Windows.Input;

namespace Books.Commands
{
    class SaveBookCommand : ICommand
    {
        Book model;
        public SaveBookCommand(Book model)
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
            DataManager.AddBook(model);
        }
    }
}

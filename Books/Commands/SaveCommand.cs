using Books.Abstraction;
using Books.Models;
using Books.ViewModels;
using System;
using System.Windows.Input;

namespace Books.Commands
{
    class SaveCommand : ICommand
    {
        IModel model;
        DataManager.Type type;
        public SaveCommand(DataManager.Type type, IModel model)
        {
            this.type = type;
            this.model = model;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (type)
            {
                case DataManager.Type.Book:
                    DataManager.AddBook((Book)model);
                    break;
                case DataManager.Type.Author:
                    DataManager.AddAuthor((Author)model);
                    break;
                case DataManager.Type.House:
                    DataManager.AddHouse((House)model);
                    break;
            }
        }
    }
}

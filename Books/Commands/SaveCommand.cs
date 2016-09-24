using Books.Abstraction;
using Books.Models;
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
        public event EventHandler CanExecuteChanged { add { } remove { } }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (type)
            {
                case DataManager.Type.Book:
                    if (DataManager.Books.ContainsKey(model.Name))
                        DataManager.RemoveBook(model.Name);
                    DataManager.AddBook((Book)model);
                    break;
                case DataManager.Type.Author:
                    if (DataManager.Authors.ContainsKey(model.Name))
                        DataManager.RemoveAuthor(model.Name);
                    DataManager.AddAuthor((Author)model);
                    break;
                case DataManager.Type.House:
                    if (DataManager.Houses.ContainsKey(model.Name))
                        DataManager.RemoveHouse(model.Name);
                    DataManager.AddHouse((House)model);
                    break;
            }
        }
    }
}

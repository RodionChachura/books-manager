using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Books.Commands
{
    class RemoveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        string name;
        DataManager.Type type;
        public RemoveCommand(DataManager.Type type, string name)
        {
            this.type = type;
            this.name = name;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (type)
            {
                case DataManager.Type.Book:
                    if (DataManager.Books.ContainsKey(name))
                        DataManager.RemoveBook(name);
                    break;
                case DataManager.Type.Author:
                    if (DataManager.Authors.ContainsKey(name))
                        DataManager.RemoveAuthor(name);
                    break;
                case DataManager.Type.House:
                    if (DataManager.Houses.ContainsKey(name))
                        DataManager.RemoveHouse(name);
                    break;
            }
        }
    }
}

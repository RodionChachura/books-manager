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
        public event EventHandler CanExecuteChanged;
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
                    DataManager.RemoveBook(name);
                    break;
                case DataManager.Type.Author:
                    DataManager.RemoveAuthor(name);
                    break;
                case DataManager.Type.House:
                    DataManager.RemoveHouse(name);
                    break;
            }
        }
    }
}

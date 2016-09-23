using Books.Models;
using Books.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Books.Commands
{
    class SaveAuthorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Author model;
        public SaveAuthorCommand(Author model)
        {
            this.model = model;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataManager.AddAuthor(model);
        }
    }
}

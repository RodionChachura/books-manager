using System;
using System.Globalization;
using System.Windows.Input;

namespace Books.Commands
{
    class languageCommand : ICommand
    {
        private static bool english = true;
        public languageCommand()
        {
        }
        public event EventHandler CanExecuteChanged { add { } remove { } }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TranslationSource.Instance.CurrentCulture = new CultureInfo(english ? "ru-RU" : "en-US");
            english = !english;
        }
    }
}

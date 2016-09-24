using Books.Abstraction;
using Books.Commands;
using Books.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace Books.ViewModels
{
    public class BookViewModel : IViewModel, INotifyPropertyChanged, IDataErrorInfo
    {
        private Book model = new Book();
        public IModel Model
        {
            get { return model; }
            set { model = (Book)value; }
        }
        public BookViewModel()
        {
            validProperties = new Dictionary<string, bool>();
            validProperties.Add(nameof(Name), false);
            validProperties.Add(nameof(Authors), false);
            validProperties.Add(nameof(ISBN), false);
            validProperties.Add(nameof(Pages), false);
            validProperties.Add(nameof(Tags), false);
            validProperties.Add(nameof(PublicationYear), false);
            validProperties.Add(nameof(House), false);
        }

        #region Properties
        public string Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Authors
        {
            get
            {
                return string.Join(", ", model.Authors);
            }
            set
            {
                model.Authors = new List<string>(FilesManager.SplitByRegex(value, ", "));
                RaisePropertyChanged(nameof(Authors));
            }
        }
        public string ISBN
        {
            get { return model.ISBN; }
            set
            {
                model.ISBN = value;
                RaisePropertyChanged(nameof(ISBN));
            }
        }
        public int Pages
        {
            get { return model.Pages; }
            set
            {
                model.Pages = value;
                RaisePropertyChanged(nameof(Pages));
            }
        }
        public string Tags
        {
            get
            {
                return string.Join(", ", model.Tags);
            }
            set
            {
                model.Tags = new List<string>(FilesManager.SplitByRegex(value, ", "));
                RaisePropertyChanged(nameof(Tags));
            }
        }
        public int PublicationYear
        {
            get { return model.PublicationYear; }
            set
            {
                model.PublicationYear = value;
                RaisePropertyChanged(nameof(PublicationYear));
            }
        }
        public string House
        {
            get
            {
                return model.House;
            }
            set
            {
                model.House = value;
                RaisePropertyChanged(nameof(House));
            }
        }
        #endregion

        #region Validation
        private bool allPropertiesValid = false;
        public string Error
        {
            get
            {
                return (model as IDataErrorInfo).Error;
            }
        }

        private Dictionary<string, bool> validProperties;
        private void ValidateProperties()
        {
            foreach (bool isValid in validProperties.Values)
            {
                if (isValid == false)
                {
                    AllPropertiesValid = false;
                    return;
                }
            }
            AllPropertiesValid = true;
        }
        public string this[string propertyName]
        {
            get
            {
                string error = (model as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public bool AllPropertiesValid
        {
            get { return allPropertiesValid; }
            set
            {
                if (allPropertiesValid != value)
                {
                    allPropertiesValid = value;
                    RaisePropertyChanged("AllPropertiesValid");
                }
            }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = (model as IDataErrorInfo)[propertyName];
                validProperties[propertyName] = string.IsNullOrEmpty(error) ? true : false;
                ValidateProperties();
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            Volatile.Read(ref PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new SaveCommand(DataManager.Type.Book, model);
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return new RemoveCommand(DataManager.Type.Book, model.Name);
            }
        }

        #endregion
    }
}

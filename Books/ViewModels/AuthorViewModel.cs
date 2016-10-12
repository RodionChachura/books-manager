using Books.Abstraction;
using Books.Commands;
using Books.Models;
using Books.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;

namespace Books.ViewModels
{
    public class AuthorViewModel : IViewModel, INotifyPropertyChanged, IDataErrorInfo
    {
        private ValidatableAuthor model = new ValidatableAuthor();
        private string dayOfBirdth = "";
        public IModel Model
        {
            get { return model; }
            set
            {
                model = new ValidatableAuthor((Author)value);
            }
        }
        public AuthorViewModel()
        {
            validProperties = new Dictionary<string, bool>();
            validProperties.Add("Name", false);
            validProperties.Add("DayOfBirdth", false);
            validProperties.Add("Books", false);
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

        public string DayOfBirdth
        {
            get
            {
                if (dayOfBirdth != "")
                    return dayOfBirdth;
                else if (model.DayOfBirdth != DateTime.MinValue)
                    return model.DayOfBirdth.ToString("dd/MM/yyyy");
                else
                    return "";
            }
            set
            {
                DateTime temp;
                DateTime.TryParseExact(value, DataManager.dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp);
                model.DayOfBirdth = temp;
                dayOfBirdth = value;
                RaisePropertyChanged(nameof(DayOfBirdth));
            }
        }

        public string Photo
        {
            get
            {
                if (model.Photo == null)
                    return FilesManager.unknownAuthorPhoto;
                else
                    return model.Photo;
            }
            set
            {
                model.Photo = value;
                RaisePropertyChanged(nameof(Photo));
            }
        }

        public string Books
        {
            get
            {
                return string.Join(", ", model.Books);
            }
            set
            {
                model.Books = new List<string>(FilesManager.SplitByRegex(value, ",  "));
                RaisePropertyChanged(nameof(Books));
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

        public string this[string propertyName]
        {
            get
            {
                string error = (model as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new SaveCommand(DataManager.Type.Author, model);
            }
        }
        public ICommand RemoveCommand
        {
            get
            {
                return new RemoveCommand(DataManager.Type.Author, model.Name);
            }
        }
        #endregion
    }
}

using Books.Abstraction;
using Books.Commands;
using Books.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using System;

namespace Books.ViewModels
{
    public class HouseViewModel : IViewModel, INotifyPropertyChanged, IDataErrorInfo
    {
        private House model = new House();
        public IModel Model
        {
            get { return model; }
            set { model = (House)value; }
        }
        public HouseViewModel()
        {
            validProperties = new Dictionary<string, bool>();
            validProperties.Add("Name", false);
            validProperties.Add("City", false);
            validProperties.Add("Books", false);
        }

        #region Properties
        public string Name
        {
            get { return model.Name; }
            set
            {
                if (model.Name != value)
                {
                    model.Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }
        public string City
        {
            get { return model.City; }
            set
            {
                if (model.City != value)
                {
                    model.City = value;
                    RaisePropertyChanged(nameof(City));
                }
            }
        }
        public string Books
        {
            get
            {
                return string.Join(",  ", model.Books);
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
                return new SaveCommand(DataManager.Type.House, model);
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return new RemoveCommand(DataManager.Type.House, model.Name);
            }
        }
        #endregion
    }
}

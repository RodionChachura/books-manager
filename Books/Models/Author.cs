using Books.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Books.Models
{
    public class Author : IModel, IDataErrorInfo
    {
        public Author()
        {
            Books = new List<string>();
        }

        #region Properties
        public string Name { get; set; }
        public DateTime DayOfBirdth { get; set; }
        public string Photo { get; set; }
        public List <string> Books { get; set; }
        #endregion

        #region Validation
        private string error;
        public string Error
        {
            get { return error; }
            private set { error = value; }
        }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "Name": ValidateName(); break;
                    case "DayOfBirdth": ValidateDayOfBirdth(); break;
                    case "Books": ValidateBooks(); break;
                }
                return error;
            }
        }

        void ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
                error = "Name can't be empty";
            else if (Name.Length < 1 || Name.Length > 50)
                error = "Invalid name size";
            else
                error = "";
        }
        private void ValidateDayOfBirdth()
        {
            if (DayOfBirdth == DateTime.MinValue)
                error = "Invalid data";
            else
                error = "";
        }
        void ValidateBooks()
        {
            if (Books.Count == 0 || Books[0] == "")
                error = "Books can't be empty";
            else
                error = "";
        }

        #endregion
    }
}

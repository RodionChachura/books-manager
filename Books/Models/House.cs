using Books.Abstraction;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace Books.Models
{
    public class House : IModel, IDataErrorInfo
    {
        public House()
        {
            Books = new List<string>();
        }

        #region Properties
        public string Name { get; set; }
        public string City { get; set; }
        public List<string> Books { get; set; }
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
                    case "City": ValidateCity(); break;
                    case "Books": ValidateBooks(); break;
                    default:
                        throw new ApplicationException("Unknown Property being validated on House.");
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
        void ValidateCity()
        {
            if (string.IsNullOrEmpty(City))
                error = "City can't be empty";
            else if (City.Length < 1 || City.Length > 50)
                error = "Invalid city size";
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

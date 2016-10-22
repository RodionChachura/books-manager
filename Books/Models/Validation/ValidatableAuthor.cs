using System;
using System.ComponentModel;

namespace Books.Models.Validation
{
    public class ValidatableAuthor : Author, IDataErrorInfo
    {
        public ValidatableAuthor() : base() { }
        public ValidatableAuthor(Author author)
        {
            Name = author.Name;
            DayOfBirdth = author.DayOfBirdth;
            Photo = author.Photo;
            Books = author.Books;
        }
        public string Error { get; private set; }

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
                return Error;
            }
        }

        void ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
                Error = "Name can't be empty";
            else if (Name.Length < 1 || Name.Length > 50)
                Error = "Invalid name size";
            else
                Error = "";
        }
        private void ValidateDayOfBirdth()
        {
            if (DayOfBirdth == DateTime.MinValue)
                Error = "Invalid data";
            else
                Error = "";
        }
        void ValidateBooks()
        {
            if (Books.Count == 0 || Books[0] == "")
                Error = "Books can't be empty";
            else
                Error = "";
        }
    }
}

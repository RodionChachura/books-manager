using Books.Abstraction;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;

namespace Books.Models
{
    public class Book : IModel, IDataErrorInfo
    {
        public Book()
        {
            Authors = new List<string>();
            Tags = new List<string>();
        }

        #region Properties
        public string Name { get; set; }
        public List<string> Authors { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public List<string> Tags { get; set; }
        public int PublicationYear { get; set; }
        public string House { get; set; }
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
                    case "Authors": ValidateAuthors(); break;
                    case "ISBN": ValidateISBN(); break;
                    case "Pages": ValidatePages(); break;
                    case "Tags": ValidateTags(); break;
                    case "PublicationYear": ValidatePublicationYear(); break;
                    case "House": ValidateHouse(); break;
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
            else if (DataManager.Books.ContainsKey(Name))
                error = "Database already contains book whith this name";
            else
                error = "";
        }
        void ValidateAuthors()
        {
            if (Authors.Count == 0 || Authors[0] == "")
                error = "Authors can't be empty";
            else
                error = "";
        } 
        void ValidateISBN()
        {
            if (ISBN == null || ISBN.Length != 10 || ISBN.Any(ch => ch < '0' || ch > '9'))
                error = "ISBN need to contain 10 digits";
            else
                error = "";
        }
        void ValidatePages()
        {
            if (Pages < 10)
                error = "Too few pages";
            else if (Pages > 13095)
                error = "Too much pages";
            else
                error = "";
        }

        void ValidateTags()
        {
            if (Tags.Count == 0 || Authors[0] == "")
                error = "Tags can't be empty";
            else
                error = "";
        }

        void ValidatePublicationYear()
        {
            if (PublicationYear > DateTime.Now.Year)
                error = "It's not Future";
            else
                error = "";
        }

        void ValidateHouse()
        {
            if (string.IsNullOrEmpty(Name))
                error = "House can't be empty";
            else if (Name.Length < 1 || Name.Length > 50)
                error = "Invalid House size";
            else
                error = "";
        }
        #endregion
    }
}

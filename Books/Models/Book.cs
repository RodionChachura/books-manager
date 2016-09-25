using Books.Abstraction;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Books.Models
{
    [Serializable]
    public class Book : IModel, IDataErrorInfo, ISerializable
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
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Authors), Authors);
            info.AddValue(nameof(ISBN), ISBN);
            info.AddValue(nameof(Pages), Pages);
            info.AddValue(nameof(Tags), Tags);
            info.AddValue(nameof(PublicationYear), PublicationYear);
            info.AddValue(nameof(House), House);
        }

        #region Validation
        [XmlIgnore]
        public string Error { get; private set; }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case nameof(Name)           : ValidateName(); break;
                    case nameof(Authors)        : ValidateAuthors(); break;
                    case nameof(ISBN)           : ValidateISBN(); break;
                    case nameof(Pages)          : ValidatePages(); break;
                    case nameof(Tags)           : ValidateTags(); break;
                    case nameof(PublicationYear): ValidatePublicationYear(); break;
                    case nameof(House)          : ValidateHouse(); break;
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
        void ValidateAuthors()
        {
            if (Authors.Count == 0 || Authors[0] == "")
                Error = "Authors can't be empty";
            else
                Error = "";
        } 
        void ValidateISBN()
        {
            if (ISBN == null || ISBN.Length != 10 || ISBN.Any(ch => ch < '0' || ch > '9'))
                Error = "ISBN need to contain 10 digits";
            else
                Error = "";
        }
        void ValidatePages()
        {
            if (Pages < 10)
                Error = "Too few pages";
            else if (Pages > 13095)
                Error = "Too much pages";
            else
                Error = "";
        }

        void ValidateTags()
        {
            if (Tags.Count == 0 || Tags[0] == "")
                Error = "Tags can't be empty";
            else
                Error = "";
        }

        void ValidatePublicationYear()
        {
            if (PublicationYear > DateTime.Now.Year)
                Error = "It's not Future";
            else
                Error = "";
        }

        void ValidateHouse()
        {
            if (string.IsNullOrEmpty(Name))
                Error = "House can't be empty";
            else if (Name.Length < 1 || Name.Length > 50)
                Error = "Invalid House size";
            else
                Error = "";
        }
        #endregion
    }
}

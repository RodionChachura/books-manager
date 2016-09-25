using Books.Abstraction;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Books.Models
{
    public class House : IModel, IDataErrorInfo, ISerializable
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(City), City);
            info.AddValue(nameof(Books), Books);
        }

        #region Validation
        public string Error { get; private set; }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "Name": ValidateName(); break;
                    case "City": ValidateCity(); break;
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
        void ValidateCity()
        {
            if (string.IsNullOrEmpty(City))
                Error = "City can't be empty";
            else if (City.Length < 1 || City.Length > 50)
                Error = "Invalid city size";
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
        #endregion
    }
}

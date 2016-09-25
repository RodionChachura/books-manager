using Books.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Books.Models
{
    [Serializable]
    public class Author : IModel, IDataErrorInfo, ISerializable
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(DayOfBirdth), DayOfBirdth);
            info.AddValue(nameof(Photo), Photo);
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
        #endregion
    }
}

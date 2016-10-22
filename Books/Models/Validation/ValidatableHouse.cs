using System.ComponentModel;

namespace Books.Models.Validation
{
    public class ValidatableHouse : House, IDataErrorInfo
    {
        public ValidatableHouse() : base() { }
        public ValidatableHouse(House house)
        {
            Name = house.Name;
            City = house.City;
            Books = house.Books;
        }

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
    }
}

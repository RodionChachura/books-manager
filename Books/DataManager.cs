using Books.Collections;
using Books.Models;
using System;
using System.Globalization;
using System.IO;

namespace Books
{
    public static class DataManager
    {
        public static readonly string[] dateFormats = { "d/m/yyyy", "dd/m/yyyy", "d/mm/yyyy", "dd/mm/yyyy",
                                                        "d/m/yyy", "dd/m/yyy", "d/mm/yyy", "dd/mm/yyy",
                                                        "d/m/yy", "dd/m/yy", "d/mm/yy", "dd/mm/yy",
                                                        "d/m/y", "dd/m/y", "d/mm/y", "dd/mm/y"};
        public enum Type { Book, Author, House}
        static public BooksCollection Books { get; private set; }
        static public AuthorsCollection Authors { get; private set; }
        static public HousesCollection Houses { get; private set; }
        static FilesManager filesManager;

        static DataManager()
        {
            filesManager = new FilesManager();

            AuthorsInit();
            HousesInit(); 
            BooksInit();
        }

        #region Initialization
        static void AuthorsInit()
        {
            Authors = new AuthorsCollection();
            foreach (string[] fields in filesManager.GetStringModelsOfAuthors())
            {
                Author author = new Author();
                author.Name = fields[0];
                DateTime date;
                string photoPath = Path.Combine(FilesManager.authorsPhotosData, author.Name.Replace(" ", "") + ".jpg");
                if (File.Exists(photoPath))
                    author.Photo = photoPath;
                if (DateTime.TryParseExact(fields[1], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    author.DayOfBirdth = date;
                    Authors.Add(author);
                }
            }
        }
        static void HousesInit()
        {
            Houses = new HousesCollection();
            foreach (string[] fields in filesManager.GetStringModelsOfHouses())
            {
                House house = new House()
                {
                    Name = fields[0],
                    City = fields[1]
                };
                Houses.Add(house);
            }
        }
        static void BooksInit()
        {
            Books = new BooksCollection();
            foreach (string[] fields in filesManager.GetStringModelsOfBooks())
            {
                Book book = new Book();
                book.Name = fields[0];

                book.ISBN = fields[2];
                int pages;
                bool result = int.TryParse(fields[3], out pages);
                if (!result)
                    break;
                else
                    book.Pages = pages;

                book.Tags.AddRange(FilesManager.SplitByRegex(fields[4], ", "));

                int year;
                result = int.TryParse(fields[5], out year);
                if (!result)
                    break;
                else
                    book.PublicationYear = year;

                Books.Add(book);
                if (Houses.ContainsKey(fields[6]))
                {
                    book.House = fields[6];
                    ((House)Houses[fields[6]]).Books.Add(book.Name);
                }
                else
                {
                    House house = new House { Name = fields[6] };
                    book.House = house.Name;
                    house.Books.Add(book.Name);
                    Houses.Add(house);
                }

                string[] stringAuthors = FilesManager.SplitByRegex(fields[1], ", ");
                foreach (string stringAuthor in stringAuthors)
                {
                    if (Authors.ContainsKey(stringAuthor))
                    {
                        book.Authors.Add(stringAuthor);
                        ((Author)Authors[stringAuthor]).Books.Add(book.Name);
                    }
                    else
                    {
                        Author newAuthor = new Author { Name = stringAuthor };
                        book.Authors.Add(stringAuthor);
                        newAuthor.Books.Add(book.Name);
                        Authors.Add(newAuthor);
                    }
                }
            }
        }
        #endregion

        #region Add/Remove authors from data(collection && file)
        static public void AddBook(Book book)
        {
            //filesManager.AddBookInFile(book);
            foreach (string author in book.Authors)
            {
                if (!Authors.ContainsKey(author))
                {
                    Author newAuthor = new Author() { Name = author };
                    newAuthor.Books.Add(author);
                    Authors.Add(newAuthor);
                }
                else
                    Authors[author].Books.Add(book.Name);
            }
            if (!Houses.ContainsKey(book.House))
            {
                House newHouse = new House() { Name = book.House };
                newHouse.Books.Add(book.House);
                Houses.Add(new House { Name = book.House });
            }
            Houses[book.House].Books.Add(book.Name);
        }
        static public void RemoveBook(string book)
        {
            foreach (string author in Books[book].Authors)
            {
                Authors[author].Books.Remove(book);
            }
            if(Houses.ContainsKey(book))
                Houses[book].Books.Remove(book);
            Books.Remove(book);
            //filesManager.RemoveBookFromFile(book);
        }
        static public void AddHouse(House house)
        {
            foreach (string book in house.Books)
            {
                if (Books.ContainsKey(book))
                {
                    Books[book].House = house.Name;
                }
                else
                {
                    Book newBook = new Book() { Name = book };
                    newBook.House = house.Name;
                    Books.Add(newBook);
                }
            }
            Houses.Add(house);
            //filesManager.AddHouseInFile(house);
        }
        static public void RemoveHouse(string house)
        {
            foreach (string book in Houses[house].Books)
            {
                Books[book].House = null;
            }
            Houses.Remove(house);
            //filesManager.RemoveHouseFromFile(house);
        }
        static public void AddAuthor(Author author)
        {
            foreach (string book in author.Books)
            {
                if (!Books.ContainsKey(book))
                {
                    Book newBook = new Book { Name = book };
                    newBook.Authors.Add(author.Name);
                    Books.Add(newBook);
                }
                else
                {
                    Books[book].Authors.Add(author.Name);
                }
            }
            Authors.Add(author);
            //filesManager.AddAuthorInFile(author);
        }
        static public void RemoveAuthor(string author)
        {
            foreach (string book in Authors[author].Books)
            {
                Books[book].Authors.Remove(Authors[author].Name);
            }
            Authors.Remove(author);
        }
        #endregion
    }
}

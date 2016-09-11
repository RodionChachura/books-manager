using Books.Collections;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Books
{
    public static class FilesManager
    {
        public static ModelsCollection authors = new ModelsCollection();
        static readonly int authorsFields = 2;

        public static ModelsCollection publicationHouses = new ModelsCollection();
        static readonly int publicationHousesFields = 2;

        public static ModelsCollection books = new ModelsCollection();
        static readonly int booksFields = 7;

        static readonly string[] dateFormats = { "d/m/yyyy", "dd/m/yyyy", "d/mm/yyyy", "dd/mm/yyy" };
        static readonly string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        static readonly string booksData = Path.Combine(dataPath, "books.txt");
        static readonly string authorsData = Path.Combine(dataPath, "authors.txt");
        static readonly string authorsPhotosData = Path.Combine(dataPath, "authorsPhotos");
        static readonly string publicationHousesData = Path.Combine(dataPath, "publicationHouses.txt");

        private static string[] splitByRegex(string text, string delim)
        {
            Regex regex = new Regex(delim);
            return regex.Split(text);
        }
        private static string[] getStringModels(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                return null;
            }
            else
            {
                string text = File.ReadAllText(filePath);
                return splitByRegex(text, Environment.NewLine + Environment.NewLine);
            }

        }
        public static void Initialization()
        {
            Directory.CreateDirectory(dataPath);
            Directory.CreateDirectory(authorsPhotosData);
            AuthorsInit();
            PublicationHousesInit();
            BooksInit();
        }

        private static void AuthorsInit()
        {
            string[] stringAuthors = getStringModels(authorsData);
            if (stringAuthors != null)
            {
                foreach (string stringAuthor in stringAuthors)
                {
                    string[] fields = splitByRegex(stringAuthor, Environment.NewLine);
                    if (fields.Length == authorsFields)
                    {
                        Author author = new Author();
                        author.Name = fields[0];
                        DateTime date;
                        string photoPath = Path.Combine(authorsPhotosData, author.Name.Replace(" ", "") + ".jpg");
                        if (File.Exists(photoPath))
                            author.Photo = Image.FromFile(photoPath);
                        if (DateTime.TryParseExact(fields[1], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        {
                            author.DayOfBirdth = date;
                            authors.Add(author);
                        }
                    }
                }
            }
        }
        private static void PublicationHousesInit()
        {
            string[] stringPublicationHouses = getStringModels(publicationHousesData);
            if (stringPublicationHouses != null)
            {
                foreach (string stringPublicationHouse in stringPublicationHouses)
                {
                    string[] fields = splitByRegex(stringPublicationHouse, Environment.NewLine);
                    if (fields.Length == publicationHousesFields)
                    {
                        PublicationHouse publicationHouse = new PublicationHouse()
                        {
                            Name = fields[0],
                            City = fields[1]
                        };
                        publicationHouses.Add(publicationHouse);
                    }
                }
            }

        }
        private static void BooksInit()
        {
            string[] stringBooks = getStringModels(booksData);
            if (stringBooks != null)
            {
                foreach (string stringBook in stringBooks)
                {
                    string[] fields = splitByRegex(stringBook, Environment.NewLine);
                    if (fields.Length == booksFields)
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

                        book.Tags.AddRange(splitByRegex(fields[4], ", "));

                        int year;
                        result = int.TryParse(fields[5], out year);
                        if (!result)
                            break;
                        else
                            book.PublicationYear = year;

                        books.Add(book);
                        if (publicationHouses.ContainsKey(fields[6]))
                        {
                            book.PublicationHouse = (PublicationHouse)publicationHouses[fields[6]];
                            ((PublicationHouse)publicationHouses[fields[6]]).Books.Add(book);
                        }
                        else
                        {
                            PublicationHouse house = new PublicationHouse { Name = fields[6] };
                            book.PublicationHouse = house;
                            house.Books.Add(book);
                            publicationHouses.Add(house);
                        }

                        string[] stringAuthors = splitByRegex(fields[1], ", ");
                        foreach(string stringAuthor in stringAuthors)
                        {
                            if (authors.ContainsKey(stringAuthor))
                            {
                                book.Authors.Add((Author)authors[stringAuthor]);
                                ((Author)authors[stringAuthor]).Books.Add(book);
                            }
                            else
                            {
                                Author newAuthor = new Author { Name = stringAuthor };
                                book.Authors.Add(newAuthor);
                                newAuthor.Books.Add(book);
                                authors.Add(newAuthor);
                            }
                        }
                    }
                }
            }
        }
    }
}

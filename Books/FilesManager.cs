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
    public static class Manager
    {
        public static ModelsCollection authors = new ModelsCollection();
        static readonly int authorsFields = 2;

        public static ModelsCollection houses = new ModelsCollection();
        static readonly int housesFields = 2;

        public static ModelsCollection books = new ModelsCollection();
        static readonly int booksFields = 7;

        static readonly string[] dateFormats = { "d/m/yyyy", "dd/m/yyyy", "d/mm/yyyy", "dd/mm/yyy" };
        static readonly string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        static readonly string booksData = Path.Combine(dataPath, "books.txt");
        static readonly string authorsData = Path.Combine(dataPath, "authors.txt");
        static readonly string authorsPhotosData = Path.Combine(dataPath, "authorsPhotos");
        static readonly string housesData = Path.Combine(dataPath, "houses.txt");

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
            HousesInit();
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
        private static void HousesInit()
        {
            string[] stringHouses = getStringModels(housesData);
            if (stringHouses != null)
            {
                foreach (string stringHouse in stringHouses)
                {
                    string[] fields = splitByRegex(stringHouse, Environment.NewLine);
                    if (fields.Length == housesFields)
                    {
                        House house = new House()
                        {
                            Name = fields[0],
                            City = fields[1]
                        };
                        houses.Add(house);
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
                        if (houses.ContainsKey(fields[6]))
                        {
                            book.House = (House)houses[fields[6]];
                            ((House)houses[fields[6]]).Books.Add(book);
                        }
                        else
                        {
                            House house = new House { Name = fields[6] };
                            book.House = house;
                            house.Books.Add(book);
                            houses.Add(house);
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

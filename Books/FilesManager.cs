using Books.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Books
{
    public class FilesManager
    {
        #region Constatnts
        readonly static string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        static readonly string separator = Environment.NewLine + Environment.NewLine;
        static readonly string booksData = Path.Combine(dataPath, "books.txt");
        static readonly string authorsData = Path.Combine(dataPath, "authors.txt");
        public static readonly string authorsPhotosData = Path.Combine(dataPath, "authorsPhotos");
        public static readonly string unknownAuthorPhoto = Path.Combine(authorsPhotosData, "unknown.jpg");
        static readonly string housesData = Path.Combine(dataPath, "houses.txt");

        const int authorsFields = 2;
        const int housesFields = 2;
        const int booksFields = 7;
        #endregion

        public FilesManager()
        {
            Directory.CreateDirectory(dataPath);
            Directory.CreateDirectory(authorsPhotosData);
        }

        public static string[] SplitByRegex(string text, string delim)
        {
            Regex regex = new Regex(delim);
            return regex.Split(text);
        }
        private string[] GetStringModels(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                return null;
            }
            else
            {
                string text = File.ReadAllText(filePath);
                return SplitByRegex(text, Environment.NewLine + Environment.NewLine);
            }

        }
        private List<string[]> GetDetailedStringModels(int numberOfFields, string source)
        {
            List<string[]> models = new List<string[]>();
            string[] strModels = GetStringModels(source);
            if(strModels != null)
            {
                foreach (string model in strModels)
                {
                    string[] fields = SplitByRegex(model, Environment.NewLine);
                    if (fields.Length == numberOfFields)
                    {
                        models.Add(fields);
                    }
                }
            }

            return models;
        }

        #region Get string representations of models
        public List<string[]> GetStringModelsOfHouses()
        {
            return GetDetailedStringModels(housesFields, housesData);
        }
        public List<string[]> GetStringModelsOfAuthors()
        {
            return GetDetailedStringModels(authorsFields, authorsData);
        }
        public List<string[]> GetStringModelsOfBooks()
        {
            return GetDetailedStringModels(booksFields, booksData);
        }
        #endregion

        #region Add/Remove models in text files
        public void AddBookInFile(Book book)
        {
            string[] fields = new string[]
            {
                book.Name,
                string.Join(", ", book.Authors),
                book.ISBN.ToString(),
                book.Pages.ToString(),
                string.Join(", ", book.Tags),
                book.PublicationYear.ToString(),
                book.House
            };

            string text = string.Join(Environment.NewLine, fields) + separator;
            File.AppendText(booksData).Write(text);
        }
        public void RemoveBookFromFile(string book)
        {
            List<string> lines = File.ReadAllLines(booksData).ToList();
            int index = lines.IndexOf(book);
            if (index != -1)
            {
                lines.RemoveRange(index, booksFields + 1);
                File.WriteAllLines(booksData, lines);
            }
        }
        public void AddAuthorInFile(Author author)
        {
            string[] fields = new string[]
            {
                author.Name,
                author.DayOfBirdth.ToString()
            };

            string text = string.Join(Environment.NewLine, fields) + separator;
            File.AppendText(authorsData).Write(text);
        }
        public void RemoveAuthorFromFile(string author)
        {
            List<string> lines = File.ReadAllLines(authorsData).ToList();
            int index = lines.IndexOf(author);
            if (index != -1)
            {
                lines.RemoveRange(index, authorsFields + 1);
                File.WriteAllLines(authorsData, lines);
            }
        }
        public void AddHouseInFile(House house)
        {
            string[] fields = new string[]
            {
                house.Name,
                house.City
            };

            string text = string.Join(Environment.NewLine, fields) + separator;
            File.AppendText(housesData).Write(text);
        }
        public void RemoveHouseFromFile(string house)
        {
            List<string> lines = File.ReadAllLines(housesData).ToList();
            int index = lines.IndexOf(house);
            if (index != -1)
            {
                lines.RemoveRange(index, housesFields + 1);
                File.WriteAllLines(housesData, lines);
            }
        }
        #endregion
    }
}

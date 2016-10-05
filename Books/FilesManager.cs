using Books.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Configuration;

namespace Books
{
    public class FilesManager
    {
        #region Constatnts
        static readonly string path = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string dataPath = Path.Combine(path, ConfigurationManager.AppSettings["dataPath"]);
        static readonly string backupData = Path.Combine(path, ConfigurationManager.AppSettings["backupPath"]);
        public static readonly string serializationData = Path.Combine(path, ConfigurationManager.AppSettings["serializationPath"]);

        static readonly string separator = Environment.NewLine + Environment.NewLine;
        static readonly string booksData = Path.Combine(dataPath, ConfigurationManager.AppSettings["booksPath"]);
        static readonly string authorsData = Path.Combine(dataPath, ConfigurationManager.AppSettings["authorsPath"]);
        static readonly string housesData = Path.Combine(dataPath, ConfigurationManager.AppSettings["housesPath"]);
        public static readonly string authorsPhotosData = Path.Combine(path, ConfigurationManager.AppSettings["authorsPhotosPath"]);
        public static readonly string unknownAuthorPhoto = Path.Combine(authorsPhotosData, ConfigurationManager.AppSettings["unknownPath"]);

        const int authorsFields = 2;
        const int housesFields = 2;
        const int booksFields = 7;
        #endregion

        public FilesManager()
        {
            Directory.CreateDirectory(dataPath);
            Directory.CreateDirectory(authorsPhotosData);
            Directory.CreateDirectory(serializationData);
        }

        #region Helping Methods
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
        private void SaveFieldsInFile(string[] fields, string filePath)
        {
            string text = separator + string.Join(Environment.NewLine, fields);
            using (StreamWriter sw = File.AppendText(housesData))
            {
                sw.WriteLine(text);
            }
        }
        private void RemoveFieldsFromFile(string name, string filePath, int numberOfFields)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            int index = lines.IndexOf(name);
            if (index != -1)
            {
                lines.RemoveRange((index == 0) ? index : index - 1, numberOfFields + 1);
                File.WriteAllLines(filePath, lines);
            }
        }
        #endregion

        #region Backup
        public void Backup()
        {
            File.Delete(backupData);
            ZipFile.CreateFromDirectory(dataPath, backupData);
        }
        public void Restore()
        {
            Directory.Delete(dataPath, true);
            ZipFile.ExtractToDirectory(backupData, dataPath);
        }
        #endregion
        
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

            SaveFieldsInFile(fields, booksData);
        }
        public void RemoveBookFromFile(string book)
        {
            RemoveFieldsFromFile(book, booksData, booksFields);
        }
        public void AddAuthorInFile(Author author)
        {
            string[] fields = new string[]
            {
                author.Name,
                author.DayOfBirdth.ToString()
            };

            SaveFieldsInFile(fields, authorsData);
        }
        public void RemoveAuthorFromFile(string author)
        {
            RemoveFieldsFromFile(author, authorsData, authorsFields);
        }
        public void AddHouseInFile(House house)
        {
            string[] fields = new string[]
            {
                house.Name,
                house.City
            };
            SaveFieldsInFile(fields, housesData);
        }
        public void RemoveHouseFromFile(string house)
        {
            RemoveFieldsFromFile(house, housesData, housesFields);
        }
        #endregion
    }
}

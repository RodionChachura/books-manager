using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Books
{
    public static class Serializator
    {
        private static string GetFile(string name, string extension)
        {
            return Path.Combine(FilesManager.serializationData, name + "." + extension);
        }

        #region Binary
        public static void SerializeToBinary(object obj, string name)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(GetFile(name, "bin"), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, obj);
            }
        }
        public static object DeserializeFromBinary(string file)
        {
            IFormatter formatter = new BinaryFormatter();
            if (File.Exists(file))
            {
                using (Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return formatter.Deserialize(stream);
                }
            }
            else
                return null;
        }
        #endregion

        #region XML
        public static void SerializeToXml<T>(T obj, string name)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(GetFile(name, "xml")))
            {
                serializer.Serialize(writer, obj);
            }
        }
        public static T DeserializeFromXml<T>(string file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            if (File.Exists(file))
            {
                using (TextReader reader = new StreamReader(file))
                {
                    return (T)deserializer.Deserialize(reader);
                }
            }
            else
                return default(T);
        }
        #endregion

        #region JSON
        public static void SerializeToJson<T>(T obj, string name)
        {
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(GetFile(name, "json"), json);
        }
        public static T DeserializeFromJson<T>(string file)
        {
            if (File.Exists(file))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
            else
                return default(T);
        }
        #endregion
    }
}

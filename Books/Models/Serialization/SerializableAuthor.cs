using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Books.Models.Serialization
{
    [Serializable]
    public class SerializableAuthor : Author, ISerializable
    {
        public SerializableAuthor() : base() { }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(DayOfBirdth), DayOfBirdth);
            info.AddValue(nameof(Photo), Photo);
            info.AddValue(nameof(Books), Books);
        }

        public SerializableAuthor(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(nameof(Name));
            DayOfBirdth = info.GetDateTime(nameof(DayOfBirdth));
            Photo = info.GetString(nameof(Photo));
            Books = (List<string>)info.GetValue(nameof(Books), typeof(List<string>));
        }
    }
}

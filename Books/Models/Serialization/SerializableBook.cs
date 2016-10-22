using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Books.Models.Serialization
{
    [Serializable]
    public class SerializableBook : Book, ISerializable
    {
        public SerializableBook() : base() { }

        public SerializableBook(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(nameof(Name));
            Authors = (List<string>)info.GetValue(nameof(Authors), typeof(List<string>));
            ISBN = info.GetString(nameof(ISBN));
            Pages = info.GetInt32(nameof(Pages));
            Tags = (List<string>)info.GetValue(nameof(Tags), typeof(List<string>));
            PublicationYear = info.GetInt32(nameof(PublicationYear));
            House = info.GetString(nameof(House));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Authors), Authors);
            info.AddValue(nameof(ISBN), ISBN);
            info.AddValue(nameof(Pages), Pages);
            info.AddValue(nameof(Tags), Tags);
            info.AddValue(nameof(PublicationYear), PublicationYear);
            info.AddValue(nameof(House), House);
        }
    }
}

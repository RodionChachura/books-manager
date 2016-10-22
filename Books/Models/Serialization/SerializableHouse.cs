using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Books.Models.Serialization
{
    [Serializable]
    public class SerializableHouse : House, ISerializable
    {
        public SerializableHouse() : base() { }
        public SerializableHouse(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(nameof(Name));
            City = info.GetString(nameof(City));
            Books = (List<string>)info.GetValue(nameof(Books), typeof(List<string>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(City), City);
            info.AddValue(nameof(Books), Books);
        }
    }
}

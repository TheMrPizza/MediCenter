using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Doctor : IPerson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Birthday")]
        public DateTime Birthday { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        public Doctor(string objectId, string name, DateTime birthday, string address)
        {
            ObjectId = objectId;
            Name = name;
            Birthday = birthday;
            Address = address;
        }
    }
}

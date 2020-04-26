using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Medicine
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public Medicine(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Medicine()
        {

        }
    }
}

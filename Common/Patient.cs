using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Patient : IPerson
    {
        [BsonId]
        public string Username { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [BsonElement("Birthday")]
        [JsonPropertyName("Birthday")]
        public DateTime Birthday { get; set; }

        [BsonElement("Address")]
        [JsonPropertyName("Address")]
        public string Address { get; set; }

        public Patient()
        {

        }

        public Patient(string username, string password, string name, DateTime birthday, string address)
        {
            Username = username;
            Password = password;
            Name = name;
            Birthday = birthday;
            Address = address;
        }
    }
}

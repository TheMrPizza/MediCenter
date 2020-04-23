using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Doctor : IPerson
    {
        [BsonId]
        public string Username { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Birthday")]
        public DateTime Birthday { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("Visits")]
        public List<Visit> Visits { get; set; }

        public Doctor(string username, string password, string name, DateTime birthday, string address)
        {
            Username = username;
            Password = password;
            Name = name;
            Birthday = birthday;
            Address = address;
            Visits = new List<Visit>();
        }

        public Doctor()
        {

        }
    }
}

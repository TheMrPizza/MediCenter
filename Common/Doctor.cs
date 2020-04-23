﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Doctor : IPerson
    {
        [BsonId]
        public string Username { get; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Birthday")]
        public DateTime Birthday { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        public Doctor(string username, string password, string name, DateTime birthday, string address)
        {
            Username = username;
            Password = password;
            Name = name;
            Birthday = birthday;
            Address = address;
        }

        public Doctor()
        {

        }
    }
}

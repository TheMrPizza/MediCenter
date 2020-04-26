﻿using System;
using System.Collections.Generic;
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

        [BsonElement("Specialities")]
        public List<Speciality> Specialities { get; set; }

        [BsonElement("VisitsId")]
        public List<string> VisitsId { get; set; }

        public Doctor(string username, string password, string name, DateTime birthday, string address,
                      List<Speciality> specialities)
        {
            Username = username;
            Password = password;
            Name = name;
            Birthday = birthday;
            Address = address;
            Specialities = specialities;
            VisitsId = new List<string>();
        }

        public Doctor()
        {

        }
    }
}

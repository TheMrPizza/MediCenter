using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Visit
    {
        [BsonId]
        public string Id { get; }

        [BsonElement("Patient")]
        public Patient Patient { get; set; }

        [BsonElement("Doctor")]
        public Doctor Doctor { get; set; }

        [BsonElement("Medicines")]
        public List<Medicine> Medicines { get; set; }

        [BsonElement("Speciality")]
        public Specialities Speciality { get; set; }

        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }

        public Visit()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Visit
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("Patient")]
        public string PatientUsername { get; set; }

        [BsonElement("Doctor")]
        public string DoctorUsername { get; set; }

        [BsonElement("Medicines")]
        public List<string> MedicinesId { get; set; }

        [BsonElement("Speciality")]
        public Speciality Speciality { get; set; }

        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }

        public Visit(string patientUsername, Speciality speciality, DateTime startTime, DateTime endTime)
        {
            Id = Guid.NewGuid().ToString();
            PatientUsername = patientUsername;
            MedicinesId = new List<string>();
            Speciality = speciality;
            StartTime = startTime.ToUniversalTime();
            EndTime = endTime.ToUniversalTime();
        }

        public Visit()
        {

        }
    }
}

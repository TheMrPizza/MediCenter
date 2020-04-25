using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public class Visit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Patient")]
        public string PatientUsername { get; set; }

        [BsonElement("Doctor")]
        public string DoctorUsername { get; set; }

        [BsonElement("Medicines")]
        public List<Medicine> Medicines { get; set; }

        [BsonElement("Speciality")]
        public Speciality Speciality { get; set; }

        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }

        public Visit(string patientUsername, Speciality speciality, DateTime startTime, DateTime endTime)
        {
            PatientUsername = patientUsername;
            Medicines = new List<Medicine>();
            Speciality = speciality;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Visit()
        {

        }
    }
}

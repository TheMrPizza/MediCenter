using System;

namespace Common
{
    public class Doctor : IPerson
    {
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
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

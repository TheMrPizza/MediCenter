using System;

namespace Common
{
    public class Patient : IPerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }

        public Patient(string id, string name, DateTime birthday, string address)
        {
            Id = id;
            Name = name;
            Birthday = birthday;
            Address = address;
        }
    }
}

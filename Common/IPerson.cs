using System;

namespace Common
{
    public interface IPerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
    }
}

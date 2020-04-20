using System;

namespace Common
{
    public interface IPerson : IModel
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
    }
}

using System;

namespace Common
{
    public interface IPerson
    {
        string Username { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        DateTime Birthday { get; set; }
        string Address { get; set; }
    }
}

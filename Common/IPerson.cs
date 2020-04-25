using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Common
{
    public interface IPerson
    {
        string Username { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        DateTime Birthday { get; set; }
        string Address { get; set; }
        List<ObjectId> VisitsId { get; set; }
    }
}

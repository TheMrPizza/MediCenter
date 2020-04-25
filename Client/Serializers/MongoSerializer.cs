//using System;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Collections.Generic;
//using System.Net.Http;
//using MongoDB.Bson;
//using System.Text;
//using System.Threading.Tasks;

//namespace Client.Serializers
//{
//    public class MongoSerializer : ISerializer
//    {
//        public HttpContent Serialize(object obj)
//        {
//            MemoryStream stream = new MemoryStream();
//            BinaryFormatter formatter = new BinaryFormatter();
//            formatter.Serialize(stream, obj);

//            string content = BsonSerializer.Deserialize<string>(stream);
//            return new StringContent(content, Encoding.UTF8, "application/json");
//        }

//        public Task<T> Deserialize<T>(HttpResponseMessage response)
//        {
//            BsonDocument.Parse()
//        }
//    }
//}

using System.Threading.Tasks;
using System.Net.Http;

namespace Client.Serializers
{
    public interface ISerializer
    {
        HttpContent Serialize(object obj);
        Task<T> Deserialize<T>(HttpResponseMessage response);
    }
}

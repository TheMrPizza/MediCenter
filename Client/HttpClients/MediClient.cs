using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Client.Serializers;
using Client.Exceptions;
using Common;

namespace Client.HttpClients
{
    public class MediClient
    {
        private static HttpClient _httpClient = new HttpClient();
        public IPerson User { get; set; }
        public ISerializer Serializer { get; }
        public IConfiguration Configuration { get; }

        public MediClient(ISerializer serializer, IConfiguration configuration)
        {
            Serializer = serializer;
            Configuration = configuration;
            Config();
        }

        public async Task<T> SignInAsync<T>(string username, string password, string type)
            where T: IPerson
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                "users/" + type + "/" + username + "/" + password);
            if (response.IsSuccessStatusCode)
            {
                return await Serializer.Deserialize<T>(response);
            }

            throw new NotFoundException("Username or password is incorrect");
        }

        public async Task<bool> RegisterAsync<T>(T person, string type)
            where T: IPerson
        {
            HttpResponseMessage response = await _httpClient.PostAsync(
                "users/" + type, Serializer.Serialize(person));
            return response.IsSuccessStatusCode;
        }

        private void Config()
        {
            _httpClient.BaseAddress = new Uri(Configuration["ConnectionString"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Client.Exceptions;
using Common;

namespace Client.HttpClients
{
    public class MediClient
    {
        private static HttpClient _httpClient = new HttpClient();
        public IPerson User { get; set; }
        public IConfiguration Configuration { get; }

        public MediClient(IConfiguration configuration)
        {
            Configuration = configuration;
            Config();
        }

        public async Task<T> SignInAsync<T>(string username, string password)
            where T: IPerson
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                "users/" + username + "/" + password);
            if (response.IsSuccessStatusCode)
            {
                return await Deserialize<T>(response);
            }

            throw new NotFoundException("Username or password is incorrect");
        }

        public async Task<bool> RegisterAsync<T>(T person, string type)
            where T: IPerson
        {
            HttpResponseMessage response = await _httpClient.PostAsync("users/" + type, Serialize(person));
            return response.IsSuccessStatusCode;
        }

        private void Config()
        {
            _httpClient.BaseAddress = new Uri(Configuration["ConnectionString"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpContent Serialize(object obj)
        {
            string content = JsonConvert.SerializeObject(obj);
            return new StringContent(content);
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

using System;
using System.IO;
using System.Text.Json;
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
                return await ReadAsAsync<T>(response);
            }

            throw new NotFoundException("Username or password is incorrect");
        }

        public async Task<bool> RegisterAsync<T>(T person, string type)
            where T: IPerson
        {
            HttpResponseMessage response = await _httpClient.PostAsync("users/" + type, Write(person));
            return response.IsSuccessStatusCode;
        }

        private void Config()
        {
            _httpClient.BaseAddress = new Uri(Configuration["ConnectionString"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpContent Write(object obj)
        {
            string content = JsonSerializer.Serialize(obj);
            return new StringContent(content);
        }

        private async Task<T> ReadAsAsync<T>(HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}

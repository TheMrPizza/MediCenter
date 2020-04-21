﻿using System;
using System.IO;
using System.Text;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common;

namespace Client.HttpClients
{
    public class MediClient
    {
        public IPerson User { get; set; }
        private static HttpClient _httpClient = new HttpClient();

        public MediClient()
        {
            ConfigAsync();
        }

        public async Task<IPerson> SignInAsync(string username, string password)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                "/users/" + username + "/" + password);
            if (response.IsSuccessStatusCode)
            {
                return await ReadAsAsync<IPerson>(response);
            }

            return null;
        }

        public async Task<bool> RegisterAsync(IPerson person, string type)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("/users/" + type, Write(person));
            return response.IsSuccessStatusCode;
        }

        private void ConfigAsync()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44354/api");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpContent Write(object obj)
        {
            string content = JsonSerializer.Serialize(obj);
            return new StringContent(content);
        }

        //private async HttpContent WriteAsAsync<T>(object obj)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    JsonSerializer.Seri
        //    await JsonSerializer.SerializeAsync(stream, obj);
        //    string content = Encoding.UTF8.GetString(stream.GetBuffer());
        //    return new StringContent(content);
        //}

        private async Task<T> ReadAsAsync<T>(HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}

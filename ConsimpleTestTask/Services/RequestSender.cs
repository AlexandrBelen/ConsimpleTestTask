using ConsimpleTestTask.Interfaces;
using ConsimpleTestTask.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConsimpleTestTask.Services
{
    public class RequestSender : IRequestSender
    {
        private readonly HttpClient _httpClient;
        public RequestSender(HttpClient client) => _httpClient = client;


        public async Task<ResultModel> Get(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequst(request);
        }

        private async Task<ResultModel> SendRequst(HttpRequestMessage request)
        {
            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return default;

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["Message"]);
            }

            return await response.Content.ReadFromJsonAsync<ResultModel>();
        }
    }
}
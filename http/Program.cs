using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace http
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Get();
            Console.ReadKey();
        }
        static async Task Get()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://www.baidu.com/");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }
        static async Task PostForm()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6740");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", "edwin")
            });
            HttpResponseMessage response = await client.PostAsync("/api/Membership/exists", content);
            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBodyAsText);
        }
        static async Task SendHttpRequest() {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:6740"),
                Method = HttpMethod.Post,
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Basic " },
                    { HttpRequestHeader.ContentType.ToString(), "multipart/mixed" },
                },
            };
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBodyAsText);
        }
    }
}
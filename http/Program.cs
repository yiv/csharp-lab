using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace http
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await HttpGet();
            Console.WriteLine("Hello World2!");
            Console.ReadKey();
        }
        static async Task HttpGet() {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://www.baidu.com/");
            if (response.IsSuccessStatusCode) {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }

        }
    }
}

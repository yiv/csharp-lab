using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace json
{
    class Program
    {
        static void Main(string[] args)
        {
            Json2Obj();
            Console.ReadKey();
        }
        static public void Obj2Json() {
            Product p = new Product();
            p.Name = "haha";
            p.Price = 80;
            var str = JsonConvert.SerializeObject(p);
            Console.WriteLine(str);
        }
        static public void Json2Obj()
        {
            var str = @"{'name':'apple','price':500 }";
            var p = JsonConvert.DeserializeObject<Product>(str);
            Console.WriteLine($"name = {p.Name}, price = {p.Price}");
        }
    }
    class Product {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
    }
}

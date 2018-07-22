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
            Obj2Json();
            Console.ReadKey();
        }
        static public void Obj2Json() {
            Product p = new Product();
            p.Name = "haha";
            p.Price = 80;
            var str = JsonConvert.SerializeObject(p);
            Console.WriteLine(str);
        }
    }
    class Product {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
    }
}

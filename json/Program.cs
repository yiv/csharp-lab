using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace jsontest
{
    class Program
    {
        static void Main(string[] args)
        {
            Obj2Json();
            Json2Obj();
            Console.ReadKey();
        }
        static public void Obj2Json()
        {
            Product p = new Product();
            p.Name = "haha";
            p.Price = 80;
            var str = JsonConvert.SerializeObject(p);
            Console.WriteLine(str);

            var data = new Dictionary<string, string>();
            data.Add("did", "aaaaaaaa");
            var loginReq = new LoginReq
            {
                AccountType = 1,
                Data = data,
            };
            str = JsonConvert.SerializeObject(loginReq);
            Console.WriteLine(str);
            Console.WriteLine("=========");
        }
        static public void Json2Obj()
        {
            var str = @"{'name':'apple','price':500 }";
            var p = JsonConvert.DeserializeObject<Product>(str);
            Console.WriteLine($"name = {p.Name}, price = {p.Price}");

            str = @"{'account_type':1,'data':{'did':'bbbbbbbbb'} }"; 
            var loginReq = JsonConvert.DeserializeObject<LoginReq>(str);
            Console.WriteLine($"AccountType = {loginReq.AccountType}, did = {loginReq.Data["did"]}");
            Console.WriteLine("=========");
        }

    }
    class Product
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
    }
    class LoginReq
    {
        [JsonProperty("account_type")]
        public int AccountType { get; set; }
        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }
    }
}

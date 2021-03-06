﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace http
{
    class LoginReq
    {
        [JsonProperty("account_type")]
        public int AccountType { get; set; }
        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            PostJson();
            Console.ReadKey();
        }

        static async void PostJson() {
            var data = new Dictionary<string, string>();
            data.Add("did", "aaaaaaaa");
            var loginReq = new LoginReq
            {
                AccountType = 1,
                Data = data,
            };
            var url = "http://45.32.39.136:10000/user/login";
            var str = JsonConvert.SerializeObject(loginReq);
            var content = new StringContent(str, Encoding.UTF8, "application/json");
            var httpCli = new HttpClient();
            var result = httpCli.PostAsync(url, content).Result;
            Console.WriteLine(result.StatusCode);
            Console.WriteLine((int)result.StatusCode);
            Console.WriteLine(result.Content.ReadAsStringAsync().Result);
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
        static async Task SendHttpRequestRC4()
        {
            var str = "{\"imsi\":\"460077054189144\",\"imei\":\"86547302505633\",\"mac\":\"24:09:95:37:70:44\"}";
            Encoding encoding = Encoding.UTF8;
            var bytes = RC4.Encrypt(str, "f63dfeafe6bd2f74fedcf754c89d25ad", encoding);
            Console.WriteLine(Encoding.UTF8.GetString(bytes));
            Console.WriteLine(Convert.ToBase64String(bytes));
            var request = new HttpRequestMessage
            {
                //RequestUri = new Uri("http://frontapi.poker666.in/device/v1/getDeviceID"),
                RequestUri = new Uri("http://192.168.1.51:10070/device/v1/getDeviceID"),
                Method = HttpMethod.Post,
                Headers = {
                    { HttpRequestHeader.ContentType.ToString(), "text/plain" },
                },
                Content = new ByteArrayContent(bytes),
            };
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            var responseBodyAsText = await response.Content.ReadAsStringAsync();
            var responseBodyAsBytes = await response.Content.ReadAsByteArrayAsync();
            //Console.WriteLine(responseBodyAsText);
            Console.WriteLine(Convert.ToBase64String(responseBodyAsBytes));
            Console.WriteLine(Encoding.UTF8.GetString(responseBodyAsBytes));
            var res = RC4.Encrypt(responseBodyAsBytes, "f63dfeafe6bd2f74fedcf754c89d25ad", encoding);
            Console.WriteLine();
            //Console.WriteLine(Convert.ToBase64String(res));
            Console.WriteLine(Encoding.UTF8.GetString(res));
        }
    }
    public class RC4
    {
        private byte[] keybox;
        private const int keyLen = 256;
        private Encoding encoding;

        public RC4(string pass)
        {
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");
            var ps = Encoding.UTF8.GetBytes(pass);
            encoding = Encoding.UTF8;
            keybox = GetKey(ps, keyLen);
        }

        public RC4(string pass, Encoding encoding)
        {
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");
            var ps = encoding.GetBytes(pass);
            this.encoding = encoding;
            keybox = GetKey(ps, keyLen);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encrypt(string data)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            return encrypt(encoding.GetBytes(data));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public byte[] Encrypt(string data, Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            return encrypt(encoding.GetBytes(data));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            return encrypt(data);
        }
        private byte[] encrypt(byte[] data)
        {
            byte[] mBox = new byte[keyLen];
            //Buffer.BlockCopy(keybox, 0, mBox, 0, keyLen);

            Array.Copy(keybox, mBox, keyLen);
            byte[] output = new byte[data.Length];
            int i = 0, j = 0;
            for (Int64 offset = 0; offset < data.Length; offset++)
            {
                i = (++i) & 0xFF;
                j = (j + mBox[i]) & 0xFF;
                byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;

                byte a = data[offset];
                byte b = mBox[(mBox[i] + mBox[j]) & 0xFF];
                output[offset] = (byte)((int)a ^ (int)b);
            }
            return output;
        }


        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, string pass, Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(encoding.GetBytes(data), encoding.GetBytes(pass));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, string pass)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(pass));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string pass, Encoding encoding)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(data, encoding.GetBytes(pass));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string pass)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(data, Encoding.UTF8.GetBytes(pass));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, byte[] pass)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (pass == null) throw new ArgumentNullException("pass");
            if (pass.Length == 0) throw new ArgumentNullException("pass");

            return encrypt(Encoding.UTF8.GetBytes(data), pass);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, byte[] pass, Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (pass == null) throw new ArgumentNullException("pass");
            if (pass.Length == 0) throw new ArgumentNullException("pass");

            return encrypt(encoding.GetBytes(data), pass);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, byte[] pass)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            if (pass == null) throw new ArgumentNullException("pass");
            if (pass.Length == 0) throw new ArgumentNullException("pass");
            return encrypt(data, pass);
        }

        private static byte[] encrypt(byte[] data, byte[] pass)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (pass == null) throw new ArgumentNullException("pass");

            byte[] mBox = GetKey(pass, keyLen);
            byte[] output = new byte[data.Length];
            int i = 0, j = 0;
            for (Int64 offset = 0; offset < data.Length; offset++)
            {
                i = (++i) & 0xFF;
                j = (j + mBox[i]) & 0xFF;
                byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;

                byte a = data[offset];
                byte b = mBox[(mBox[i] + mBox[j]) & 0xFF];
                output[offset] = (byte)((int)a ^ (int)b);
            }
            return output;
        }


        private static byte[] GetKey(byte[] pass, int kLen)
        {
            byte[] mBox = new byte[kLen];
            for (Int64 i = 0; i < kLen; i++)
            {
                mBox[i] = (byte)i;
            }
            Int64 j = 0;
            for (Int64 i = 0; i < kLen; i++)
            {
                j = (j + mBox[i] + pass[i % pass.Length]) % kLen;
                byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;
            }
            return mBox;
        }
    }
}
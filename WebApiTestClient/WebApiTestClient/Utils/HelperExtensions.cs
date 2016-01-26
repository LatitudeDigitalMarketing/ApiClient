using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WebApiTestClient.Models;

namespace WebApiTestClient.Utils
{
    public static class HelperExtensions
    {
        public static T Serialize<T>(this T obj, string fileName)
        {
            var objStr = JsonConvert.SerializeObject(obj);

            using (var writer = new StreamWriter(fileName))
            {
                writer.Write(objStr);
            }
            return obj;
        }


        public static T Deserialize<T>(this T obj, string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                var objStr = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<T>(objStr);
            }
        }

        
        public static async Task<T> GetFromService<T>(this T obj, string token, string baseAddress, string commandUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonString = await client.GetStringAsync(commandUrl);

                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public static async Task<T> PostToService<T>(this T obj, string token, string baseAddress, string commandUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsJsonAsync(commandUrl, obj);

                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result)
                    : HandleError<T>(response);

            }
        }

        public static async Task<T> PutToService<T>(this T obj, string token, string baseAddress, string commandUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PutAsJsonAsync(commandUrl, obj);

                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result)
                    : HandleError<T>(response);
            }
        }

        private static T HandleError<T>(HttpResponseMessage response)
        {
            response.Content.ReadAsStringAsync().Result.ConsoleRed();
            return default(T);
        }

        public static void PrintObjectToConsole<T>(this T obj)
        {
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                Console.WriteLine($"{propertyInfo.Name}: {propertyInfo.GetValue(obj)}"); 
            }

        }
    }
}
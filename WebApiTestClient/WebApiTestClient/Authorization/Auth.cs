using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebApiTestClient.Models;
using WebApiTestClient.Utils;

namespace WebApiTestClient.Authorization
{
    public class Auth
    {
        private readonly string _baseAddress;
        public Auth(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public async Task<Tokens> ResourceOwnerLogin(string user, string pass)
        {
            return await ParseResponseAndStore(GetTokenResponse(user, pass));
        }

        public async Task<Tokens> GetStoredTokens()
        {
            try
            {
                var tokens = new Tokens().Deserialize("Tokens.json");
                return tokens.ExpiryDate >= DateTime.Now
                    ? tokens
                    : await ParseResponseAndStore(GetRefreshTokenResponse(tokens.RefreshToken));
            }
            catch (Exception ex)
            {
                return new Tokens();
            }
  
        }

        private static async Task<Tokens> ParseResponseAndStore(Task<string> response)
        {
            var content = await response;

            var jsonObj = JObject.Parse(content);


            var token = jsonObj.SelectToken("$.access_token");
            var refreshToken = jsonObj.SelectToken("$.refresh_token");
            var expires = jsonObj.SelectToken("$['.expires']");

            var tokens = new Tokens
            {
                AccessToken = token.ToString(),
                RefreshToken = refreshToken.ToString(),
                ExpiryDate = DateTime.Parse(expires.ToString())
            };

            tokens.Serialize("Tokens.json");

            return tokens;
        }



        private async Task<string> GetRefreshTokenResponse(string refreshToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);

                var content = new Dictionary<string, string>();
                content.Add("grant_type", "refresh_token");
                content.Add("refresh_token", refreshToken);
                content.Add("client_id", "PartnerApiClient");

                var response = await client.PostAsync("token", new FormUrlEncodedContent(content));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
                //var responseContent = await response.Content.ReadAsStringAsync();
                //return await Task.Run(() => responseContent);

            }
        }

        private async Task<string> GetTokenResponse(string user, string pass)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);


                var content = new Dictionary<string, string>();
                content.Add("grant_type", "password");
                content.Add("username", user);
                content.Add("password", pass);
                content.Add("client_id", "PartnerApiClient");
                var response = await client.PostAsync("token", new FormUrlEncodedContent(content));


                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
                //var responseContent = await response.Content.ReadAsStringAsync();
                //return await Task.Run(() => responseContent);
            }
        }
    }
}

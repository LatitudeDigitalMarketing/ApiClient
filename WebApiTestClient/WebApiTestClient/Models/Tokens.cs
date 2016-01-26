using System;
using System.Xml.Serialization;

namespace WebApiTestClient.Models
{
    
    public class Tokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}

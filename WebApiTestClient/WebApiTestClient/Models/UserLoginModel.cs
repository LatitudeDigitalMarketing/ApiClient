namespace WebApiTestClient.Models
{
    public class UserLoginModel
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        //public string ClientId { get; set; }
    }
}

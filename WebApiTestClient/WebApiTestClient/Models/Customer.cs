namespace WebApiTestClient.Models
{
    public class Customer
    {
        public int? CustomerId { get; set; }
        public string CustomerRef { get; set; }
        public string BusinessName { get; set; }
        public string BusinessWebsite { get; set; }
        public string ContactName { get; set; }
        public string SendReportEmail { get; set; }
        public string IndustryName { get; set; }
        public string Market { get; set; }
    }
}
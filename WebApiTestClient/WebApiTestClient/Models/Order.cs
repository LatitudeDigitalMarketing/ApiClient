using System;

namespace WebApiTestClient.Models
{
    public class Order
    {
        public int CustomerId { get; set; }
        public int CampaignId { get; set; }
        public int? OrderId { get; set; }
        public string OrderRef { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal SpendTarget { get; set; }
        public int? ClickTarget { get; set; }
        public decimal ManagementFee { get; set; }
        public string CampaignNotes { get; set; }
        public string Package { get; set; }
        public string PackageType { get; set; }
        public Status Status { get; set; }
        public Cancel Cancel { get; set; }
        public GeoTargetting GeoTargetting { get; set; }
    }
}
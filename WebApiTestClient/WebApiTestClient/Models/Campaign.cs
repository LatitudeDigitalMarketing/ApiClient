﻿using System.Collections.Generic;

namespace WebApiTestClient.Models
{
    public class Campaign
    {
        public int? CustomerId { get; set; }
        public int? Id { get; set; }
        public string CampaignName { get; set; }
        public string CampaignUrl { get; set; }
        public string Product { get; set; }
        public string CurrencyCode { get; set; }
        public List<CampaignTarget> CampaignTargets { get; set; }
    }
}
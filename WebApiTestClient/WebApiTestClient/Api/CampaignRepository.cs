using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTestClient.Models;
using WebApiTestClient.Utils;

namespace WebApiTestClient.Api
{
    internal class CampaignRepository
    {
        private readonly string _baseAddress;
        private readonly string _token;

        public CampaignRepository(string baseAddress, string token)
        {
            _baseAddress = baseAddress;
            _token = token;
        }

        public Task<Campaign> GetCampaignById(int id)
        {
            return new Campaign().GetFromService(_token, _baseAddress, $"api/v1/campaigns/{id}");
        }

        public Task<Campaign> CreateCampaign(Campaign campaign)
        {
            return campaign.PostToService(_token, _baseAddress, "api/v1/campaigns");
        }

        public Task<List<Order>> GetOrdersForCampaign(int campaignId)
        {
            return new List<Order>().GetFromService(_token, _baseAddress, $"api/v1/campaigns/{campaignId}/orders");
        }

        public Task<Campaign> UpdateCampaign(Campaign campaign)
        {
            return campaign.PutToService(_token, _baseAddress, $"api/v1/campaigns/{campaign.CampaignId}");
        } 
    }
}

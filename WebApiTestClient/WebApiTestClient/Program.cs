using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WebApiTestClient.Api;
using WebApiTestClient.Authorization;
using WebApiTestClient.Models;
using WebApiTestClient.Utils;

namespace WebApiTestClient
{
    class Program
    {
        private static readonly Uri BaseAddress = new Uri("https://localhost:44301/");// new Uri("http://localhost:56402/");//new Uri("http://localhost:26264/");
        private static Customer _customer;
        private static Campaign _campaign;
        private static Order _order;

        static void Main(string[] args)
        {

            
            var auth = new Auth(BaseAddress.AbsoluteUri);

            var tokens = auth.GetStoredTokens();
            tokens.Wait();

            if (string.IsNullOrEmpty(tokens.Result.RefreshToken))
            {
                "Username:".ConsoleGreen();
                var user = Console.ReadLine();
                "Password:".ConsoleGreen();
                var pass = Helper.ConsoleReadPassword();
                Console.WriteLine();

                tokens = auth.ResourceOwnerLogin(user, pass);
            }

            "Token:".ConsoleGreen();
            Console.WriteLine(tokens.Result.AccessToken);

            "Now we have the token lets hit the API:".ConsoleYellow();

            //GetSomeObjects(BaseAddress.AbsoluteUri, tokens.Result.AccessToken);

            //CreateANewCustomer(BaseAddress.AbsoluteUri, tokens.Result.AccessToken);
            //AddACampaignToACustomer(BaseAddress.AbsoluteUri, tokens.Result.AccessToken, _customer.CustomerId.Value);
            //AddAnOrderToACampaign(BaseAddress.AbsoluteUri, tokens.Result.AccessToken, _customer.CustomerId.Value, _campaign.CampaignId.Value);


            //AddACampaignToACustomer(BaseAddress.AbsoluteUri, tokens.Result.AccessToken, 40763);
            //AddAnOrderToACampaign(BaseAddress.AbsoluteUri, tokens.Result.AccessToken, 40763, 27404);

            UpdateSomeObjectsUsingPut(BaseAddress.AbsoluteUri, tokens.Result.AccessToken);


            Console.ReadKey();
        }

        private static void UpdateSomeObjectsUsingPut(string baseAddress, string token)
        {
            var custRepo = new CustomerRepository(baseAddress, token);
            var campaignRepo = new CampaignRepository(baseAddress, token);
            var orderRepo = new OrderRepository(baseAddress, token);

            var campaigns = custRepo.GetCampaignsForCustomer(38245).Result;

            var firstCampaign = campaigns.First();

            var orders = campaignRepo.GetOrdersForCampaign(firstCampaign.CampaignId.Value).Result;

            var firstOrder = orders.First();

            firstOrder.SpendTarget = firstOrder.SpendTarget + 100;

            orderRepo.UpdateOrder(firstOrder);

            firstCampaign.CampaignName = firstCampaign.CampaignName + " (1)";

            campaignRepo.UpdateCampaign(firstCampaign);

        }

        static void GetSomeObjects(string baseAddress, string token)
        {
            var custRepo = new CustomerRepository(baseAddress, token);
            var campaignRepo = new CampaignRepository(baseAddress, token);
            var orderRepo = new OrderRepository(baseAddress, token);

            "Get a customer".ConsoleGreen();
            custRepo.GetCustomerById(38245).Result.PrintObjectToConsole();

            "Get a campaign".ConsoleGreen();
            campaignRepo.GetCampaignById(25269).Result.PrintObjectToConsole();

            "Get an order: ".ConsoleGreen();
            orderRepo.GetOrderById(42802).Result.PrintObjectToConsole();
        }

        private static void CreateANewCustomer(string baseAddress, string token)
        {
            var custRepo = new CustomerRepository(baseAddress, token);
            "Create a customer".ConsoleBlue();
            var customer = custRepo.CreateCustomer(new Customer
            {
                CustomerRef = "MyTest101",
                BusinessName = "My Test Customer",
                ContactName = "Test",
                SendReportEmail = "test@mytestcustomer.com",
                BusinessWebsite = "www.mytestcustomer.com",
                IndustryName = "Testing",
                Market = "United Kingdom"
            });

            customer.Result?.PrintObjectToConsole();
            _customer = customer.Result;
        }

        static void AddACampaignToACustomer(string baseAddress, string token, int customerId)
        {
            var campaignRepo = new CampaignRepository(baseAddress, token);
            
            "Create a campaign".ConsoleBlue();
            var campaign = campaignRepo.CreateCampaign(new Campaign
            {
                CampaignName = "Test Campaign",
                CampaignUrl = "http://www.mytestcustomer.com",
                CurrencyCode = "GBP",
                CustomerId = customerId,
                Product = "PPC"
            });

            campaign.Result?.PrintObjectToConsole();
            _campaign = campaign.Result;
        }

        static void AddAnOrderToACampaign(string baseAddress, string token, int customerId, int campaignId)
        {
            var orderRepo = new OrderRepository(baseAddress, token);

            "Create an order".ConsoleBlue();
            var order = orderRepo.CreateOrder(new Order
            {
                CustomerId = customerId,
                CampaignId = campaignId,
                StartDate = new DateTime(2016, 02, 01),
                EndDate = new DateTime(2016, 04, 30),
                OrderRef = "MyOrderRef101",//should be unique (it will be made unique if duplicate already exists in your records), if it is not provided we will create a GUID.
                PackageType = "click",//the types are 'click', 'spend' or 'position'
                Package = "MyPackage",//what you want to call the package for reporting
                SpendTarget = 1000,
                ClickTarget = 500,// only requred for 'click' package type
                Status = new Status { Paused = true, PaymentFailed = false}
            });

            order.Result?.PrintObjectToConsole();
            _order = order.Result;
        }


    }
}

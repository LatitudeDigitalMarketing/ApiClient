using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTestClient.Models;
using WebApiTestClient.Utils;

namespace WebApiTestClient.Api
{
    internal class CustomerRepository
    {
        private readonly string _baseAddress;
        private readonly string _token;

        public CustomerRepository(string baseAddress, string token)
        {
            _baseAddress = baseAddress;
            _token = token;
        }

        public Task<Customer> GetCustomerById(int id)
        {
            return new Customer().GetFromService(_token, _baseAddress, $"api/v1/customers/{id}");
        }

        public Task<Customer> CreateCustomer(Customer customer)
        {
            return customer.PostToService(_token, _baseAddress, "api/v1/customers");
        }

        public Task<List<Campaign>> GetCampaignsForCustomer(int customerId)
        {
            return new List<Campaign>().GetFromService(_token, _baseAddress, $"api/v1/customers/{customerId}/campaigns");
        }
    }
}

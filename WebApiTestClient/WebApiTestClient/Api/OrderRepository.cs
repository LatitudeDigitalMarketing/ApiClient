using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTestClient.Models;
using WebApiTestClient.Utils;

namespace WebApiTestClient.Api
{
    internal class OrderRepository
    {
        private readonly string _baseAddress;
        private readonly string _token;

        public OrderRepository(string baseAddress, string token)
        {
            _baseAddress = baseAddress;
            _token = token;
        }

        public Task<Order> GetOrderById(int id)
        {
            return new Order().GetFromService(_token, _baseAddress, $"api/v1/orders/{id}");
        }

        public Task<Order> CreateOrder(Order order)
        {
            return order.PostToService(_token, _baseAddress, "api/v1/orders");
        }

        public Task<Order> UpdateOrder(Order order)
        {
            return order.PutToService(_token, _baseAddress, $"api/v1/orders/{order.OrderId}");
        }
    }
}

﻿using Newtonsoft.Json;
using PizzaPalace.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PizzaPalace.ViewModel
{
    class OrderViewModel
    {
        private const string URL = "https://localhost:5001/api";
        private const string ControllerName = "Orders";
        HttpClient httpClient = new HttpClient();
        public Order FormOrder { get; set; } = new Order();
        /// <summary>
        /// Posts order to backend.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> AddOrder(Order order)
        {
            //POST in Orders
            order.OrderTime = DateTime.Now;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(order));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await this.httpClient.PostAsync(URL + "/" + ControllerName, httpContent);           
            //POST in OrderItems
            order.CopyFrom(JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync()));
            foreach (var item in this.FormOrder.Items)
            {
                item.OrderID = order.OrderID;
                httpContent = new StringContent(JsonConvert.SerializeObject(item));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await this.httpClient.PostAsync(URL + "/OrderItems", httpContent);
            }
            return order;
        }
        /// <summary>
        /// Gets specific order from backend based on parameter.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task FetchOrder(int id)
        {
            var response = await this.httpClient.GetAsync(URL + "/" + ControllerName + "/" + id);
            this.FormOrder.CopyFrom(JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync()));    
        }
    }
}

using Newtonsoft.Json;
using ServiceGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ServiceGateway.Services
{
    public class TLService
    {
        private HttpClient _client;
        private readonly string _address = "TLAdresse.com";
        private readonly string _username = "TLUser";
        private readonly string _password = "TLPass";

        public TLService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }


        public async Task<RouteResponse> GetRoute(string source, string target, string shipmentDate, double weightInKg, double largestSizeinCm, string goodsType, bool recommended)
        {
            Parcel parcel = new Parcel()
            {
                ShipmentDate = shipmentDate,
                WeightInKg = weightInKg,
                LargestSizeInCm = largestSizeinCm,
                GoodsType = goodsType,
                Recommended = recommended,
            };
            RouteRequest request = new RouteRequest()
            {
                Source = source,
                Target = target,
                Parcel = parcel,
            };
            var jstring = JsonConvert.SerializeObject(request);
            var message = new HttpRequestMessage();
            message.Content = new StringContent(jstring);
            message.Headers.Add("username", _username);
            message.Headers.Add("password", _password);

            var response = await _client.SendAsync(message);
            var resultString = response.Content.ReadAsStringAsync().Result;

            var routeResp = JsonConvert.DeserializeObject<RouteResponse>(resultString);
            return routeResp;
        }
    }
}
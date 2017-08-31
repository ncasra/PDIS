﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceGateway.Controllers
{
    public class RouteRequest
    {
        public string source;
        public string target;
    }

    public class RouteResponse
    {
        public string Time;
        public string Price;
    }
    [System.Web.Http.RoutePrefix("api/test")]
    public class TestController : ApiController
    {


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("{user}/{pass}")]
        public async Task<HttpResponseMessage> GetRouteTimeAndCost(HttpRequestMessage request, string user, string pass)
        {
            //Ensure HTTPS
            bool httpsquestionmark = request.RequestUri.Scheme == Uri.UriSchemeHttps;
            //if not return statuscode forbidden
            
            HttpResponseMessage response;
            if (user != "valid" && false) //validation
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "User not valid",
                };
                return response;            
            }
            var jstring = await request.Content.ReadAsStringAsync();
            RouteRequest requestObject;
            try
            {
                requestObject = JsonConvert.DeserializeObject<RouteRequest>(jstring);
            }
            catch (Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = "Could not parse JSON",
                };
            }
            //Look-up price and time via data from requestObject
            //Fill answer
            RouteResponse answer = new RouteResponse()
            {
                Time = "1h",
                Price = "1kr",
            };
            response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                ReasonPhrase = "Godt klaret",
            };
            var jsonstring = JsonConvert.SerializeObject(answer);
            response.Content =  new StringContent(jsonstring);
            return response;

        }

    }
}

using Newtonsoft.Json;
using ServiceGateway.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceGateway.Controllers
{
    [System.Web.Http.RoutePrefix("api/ExternalServices")]
    public class ExternelServiceController : ApiController
    {
        private readonly NameValueCollection _users = (NameValueCollection)ConfigurationManager.GetSection("UserSection");


        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> GetRouteTimeAndCost(HttpRequestMessage request)
        {
            HttpResponseMessage response;
            //Ensure HTTPS
            if (!(request.RequestUri.Scheme == Uri.UriSchemeHttps))
            {
                response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTPS Required",
                };
                return response;
            }
            IEnumerable<string> users;
            var getUserHeader = request.Headers.TryGetValues("username", out users);
            if (!getUserHeader)
            {
                response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "'username' header required",
                };
                return response;
            }
            string storedPassword;
            try
            {
                storedPassword =  _users.Get(users.First());
            }
            catch (Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "Specified username not recognized",
                };
                return response;
            }
            IEnumerable<string> passes;
            var getPassHeader = request.Headers.TryGetValues("password", out passes);
            if (!getPassHeader)
            {
                response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "'password' header required",
                };
            }
            if (passes.First() != storedPassword)
            {
                response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "Incorrect password",
                };
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
                TimeInHours = 1,
                CostInDollars = 1,
                TransactionID = 1,

            };
            response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var jsonstring = JsonConvert.SerializeObject(answer);
            response.Content =  new StringContent(jsonstring);
            return response;
        }

    }
}

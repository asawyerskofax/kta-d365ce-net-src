/*
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Linq;

namespace D365CELib
{
    public class D365CELib
    {
        public string authType = "Office365";


        //public Guid Login()
        //{
        //    //var userCredential = new UserCredential(username, password);
        //    string apiVersion = "9.0";
        //    string webApiUrl = $"{url}/api/data/v{apiVersion}";

        //    var authParameters = AuthenticationParameters.CreateFromResourceUrlAsync(new Uri(webApiUrl)).Result;
        //    var authContext = new AuthenticationContext(authParameters.Authority, false);
        //    //var authResult = authContext.AcquireToken(url, appClientId, userCredential);
        //    //var authHeader = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(webApiUrl);
        //        //client.DefaultRequestHeaders.Authorization = authHeader;

        //        // use the WhoAmI function
        //        var response = client.GetAsync("WhoAmI").Result;
        //        Guid userId;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Get the repsonse content and parse it
        //            JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);
        //            //Guid userId = (Guid)body["UserId"];
        //            userId = (Guid)body["UserId"];
        //            Console.WriteLine($"Your UserId is {userId}");
        //            //return userId;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Error, press any key to exit");
        //            //return null;
        //        }
        //        //return userId;
        //    }
        //}
        private static async Task CreateRecord(HttpClient httpClient)
        {
            JObject contact1 = new JObject
            {
                { "firstname", "Demo" },
                { "lastname", "Contact" },
                { "annualincome", 100000 }
            };

            contact1["jobtitle"] = "Junior Developer";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "contacts")
            {
                Content = new StringContent(contact1.ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent) //204
            {
                Console.WriteLine("POST succeeded, entity created");
                //optionall process reponse message headers or body
                var entityUri = response.Headers.GetValues("OData - EntityId").FirstOrDefault();
            }
            else
            {
                Console.WriteLine($"Operation failed: {response.ReasonPhrase}");
            }
        }
    }
}
*/
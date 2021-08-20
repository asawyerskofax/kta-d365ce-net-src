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

namespace ConsoleDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            string authType = "Office365";
            string url = "https://orgebf85c25.crm.dynamics.com";
            string username = "adam.sawyers@kofaxtst.onmicrosoft.com";
            string password = "Hippomilk-00";
            string appClientId = "f9b3d38e-6c9c-485b-8e3d-78176ef7619e";

            //string conn = $@"Url = {url}; AuthType = {authType}; UserName = {username}; Password = {password}; RequireNewInstance = False";
            //CrmServiceClient svc = new CrmServiceClient(conn);

            //AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/common", false);
            //UserCredential credential = new UserCredential(username, password);
            //AuthenticationResult result = authContext.AcquireToken(url, appClientId, credential);
            //string accessToken = result.AccessToken;

            //var client = new CrmController();
            //string token = client.GetAuthToken();

            //var userCredential = new UserCredential(username, password);
            //string apiVersion = "9.0";
            //string webApiUrl = $"{url}/api/data/v{apiVersion}";

            //var authParameters = AuthenticationParameters.CreateFromResourceUrlAsync(new Uri(webApiUrl)).Result;
            //var authContext = new AuthenticationContext(authParameters.Authority, false);
            //var authResult = authContext.AcquireToken(url, appClientId, userCredential);
            //var authHeader = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

            //Console.WriteLine(authResult.AccessToken);

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(webApiUrl);
            //    client.DefaultRequestHeaders.Authorization = authHeader;

            //    // use the WhoAmI function
            //    var response = client.GetAsync("WhoAmI").Result;

            //    Console.WriteLine(response);
            //    Console.WriteLine(response.IsSuccessStatusCode);
            //    Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        //Get the repsonse content and parse it
            //        JObject body = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            //        Guid userId = (Guid)body["UserId"];
            //        Console.WriteLine($"Your UserId is {userId}");

            //        //CreateRecord(client);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error, press any key to exit");
            //        Console.ReadLine();
            //    }
            // }
        }
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
            Console.WriteLine(response);

            if (response.StatusCode == HttpStatusCode.NoContent) //204
            {
                Console.WriteLine("POST succeeded, entity created");
                //optionall process reponse message headers or body
                var entityUri = response.Headers.GetValues("OData - EntityId").FirstOrDefault();
                Console.WriteLine(entityUri);
            }
            else
            {
                Console.WriteLine($"Operation failed: {response.ReasonPhrase}");
            }
        }
    }
}

*/

/*
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Web.Http;

namespace MyWebApi.Controllers
{
    public class CrmController : ApiController
    {
        static string serviceUri = "https://orgebf85c25.crm.dynamics.com";
        //static string redirectUrl = "http://localhost:64884/";

        static string apiVersion = "9.1";
        static string crmapiUrl = $"{serviceUri}/api/data/v{apiVersion}/";
        public string GetAuthToken()
        {

            // TODO Substitute your app registration values that can be obtained after you
            // register the app in Active Directory on the Microsoft Azure portal.
            //Connect to crm API using Username and Password with oAuth 2.0 v1 Endpoints.
            string clientId = "f9056b0e-b8af-4c11-85ec-a1946c615b6f"; // Client ID after app registration
            string userName = "adam.sawyers@kofaxtst.onmicrosoft.com";
            string password = "Hippomilk-00";

            var credentials = new UserPasswordCredential(userName, password);
            string authority = "https://login.microsoftonline.com/03b9cd1b-6cfc-4c45-****-1092fd6f0e26/oauth2/authorize";  //oAuth 2.0 v1 Endpoints
            var context = new AuthenticationContext(authority, false);
            var authResult = context.AcquireTokenAsync(resource: serviceUri, clientId, credentials).Result;

            //Connect to crm API using secretKey with oAuth 2.0 v1 Endpoints.
            //string clientId = "edb0f75d-****-****-bcce-259e0d289148";
            //string appKey = "3c47a24c-794e-43a4-8288-d229b03a96d5"; //Client Secret
            //ClientCredential credentials = new ClientCredential(clientId, appKey);
            //string authority = "https://login.microsoftonline.com/d245e842-b71e-42df-a18e-176555cfb904/oauth2/token";
            //var authResult = new AuthenticationContext(authority, true).AcquireTokenAsync(serviceUri, credentials).Result;
            //return authResult.AccessToken;
            string guid = RetrieveAccounts(authResult.AccessToken);

            CreateContacts(authResult.AccessToken, guid);
            return authResult.AccessToken;
        }

        private void CreateContacts(string accessToken, string guid)
        {
            try
            {
                JObject contact1 = new JObject{
                                            { "firstname", "Sanket" },
                                            { "lastname", "Sinha" },
                                            { "emailaddress1", "nowsanket@gmail.com" }
                                            };

                contact1["jobtitle"] = "Junior Developer";
                contact1.Add("modifiedby@odata.bind", "/systemusers(5ef22367-aa3f-4bbe-b490-39b332c9e6a8)");
                contact1.Add("gendercode", 1);

                HttpClient httpClient = new HttpClient();
                //Default Request Headers needed to be added in the HttpClient Object
                httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Set the Authorization header with the Access Token received specifying the Credentials
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                httpClient.BaseAddress = new Uri(crmapiUrl);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "contacts")
                {
                    Content = new StringContent(contact1.ToString(), Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = httpClient.SendAsync(request).Result;
                if (response.IsSuccessStatusCode) //204
                {
                    var entityUri = response.Headers.GetValues("OData - EntityId").FirstOrDefault();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string RetrieveAccounts(string authToken)
        {
            string guid = string.Empty;
            HttpClient httpClient = new HttpClient();
            //Default Request Headers needed to be added in the HttpClient Object
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Set the Authorization header with the Access Token received specifying the Credentials
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            httpClient.BaseAddress = new Uri(crmapiUrl);
            //Examples of different filters here.
            //var response = httpClient.GetAsync("accounts?$select=name&amp;amp;amp;amp;amp;amp;$top=1").Result;
            //var response = httpClient.GetAsync("accounts?$select=name&amp;amp;amp;amp;amp;amp;$top=1").Result;
            //var response = httpClient.GetAsync("contacts?$select=fullname&amp;amp;amp;amp;amp;amp;$expand=parentcustomerid_account($select=accountid,name,createdon,emailaddress1,address1_telephone1)&amp;amp;amp;amp;amp;amp;$filter=emailaddress1 eq 'nowsanket@gmail.com'&amp;amp;amp;amp;amp;amp;$top=1").Result;
            var response = httpClient.GetAsync("importfiles?$select=successcount,name").Result;
            if (response.IsSuccessStatusCode)
            {
                var accounts = response.Content.ReadAsStringAsync().Result;

                var jRetrieveResponse = JObject.Parse(accounts);

                dynamic collContacts = JsonConvert.DeserializeObject(jRetrieveResponse.ToString());

                foreach (var data in collContacts.value)
                {
                    //You can change as per your need here
                    guid = data.importfileid.Value;
                    Console.WriteLine("Contact Name – " + data.name.Value);
                }

            }
            return guid;
        }
    }
}
*/
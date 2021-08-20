using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace D365CEApiLib
{
    public class D365CE
    {
        private string API_ENDPOINT = "/api/data/v9.1";
        static D365CE()
        {
            // SF requires TLS 1.1 or 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        }
        public string GetAuthToken(string directoryTenantId, string appClientId, string clientSecretValue, string instanceUrl)
        {
            var client = new HttpClient();

            var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", appClientId },
                    {"client_secret", clientSecretValue },
                    {"resource", instanceUrl }
                }
            );

            string loginUrl = "https://login.microsoftonline.com/" + directoryTenantId + "/oauth2/token";

            var response = client.PostAsync(loginUrl, request).Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
            string authToken = values["access_token"];
            return authToken;
        }

        public string QueryAccounts(string instanceUrl, string authToken, string accountName)
        {
            var client = new HttpClient();
            //Set the Authorization header with the Access Token received specifying the Credentials
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            //Build GET request
            string accountsEndpoint = instanceUrl + API_ENDPOINT + $"/accounts?$select=name,accountid&$filter=contains(name, '{accountName}')";
            
            var jsonResponse = client.GetAsync(accountsEndpoint).Result;
            string stringResponse = jsonResponse.Content.ReadAsStringAsync().Result;

            var jRetrieveResponse = JObject.Parse(stringResponse);
            dynamic collAccounts = JsonConvert.DeserializeObject(jRetrieveResponse.ToString());
            string accountId = collAccounts.value[0].accountid;

            //Alternative way if you want to create classes (see below)
            //QueryResponse accounts = JsonConvert.DeserializeObject<QueryResponse>(stringResponse);
            //string accountId = accounts.value[0].accountid;

            return accountId;
        }

        public Boolean CreateCase(string instanceUrl, string authToken, string title, string accountId, string description)
        {
            Boolean createCaseSuccess = false;
            var client = new HttpClient();

            var requestBody = new Dictionary<string, string>
                {
                    {"title", title},
                    {"customerid_account@odata.bind", $"/accounts({accountId})" },
                    {"description", description }
                };

            string requestBodyStr = JsonConvert.SerializeObject(requestBody);
            string createCaseEndpoint = instanceUrl + API_ENDPOINT + "/incidents";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, createCaseEndpoint);
            //Set the Authorization header with the Access Token received specifying the Credentials
            request.Headers.Add("Authorization", "Bearer " + authToken);
            request.Content = new StringContent(requestBodyStr, Encoding.UTF8, "application/json");

            var result = client.SendAsync(request).Result;
            var jsonResponse = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                createCaseSuccess = true;
            }

            return createCaseSuccess;
        }
    }

    //Dont need these unless you wanna do it this way
    public class QueryResponse
    {
        public List<Account> value { get; set; }
    }
    public class Account
    {
        public string name { get; set; }
        public string accountid { get; set; }
    }
}

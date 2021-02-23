using System;
using System.Net;
using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Codetest{

    public class TestAPI{

        // 1 - Get each country (US, DE and GB) individually and validate the response
        [TestCase ("US", TestName = "Validate API US Country")]
        [TestCase ("DE", TestName = "Validate API DE Country")]
        [TestCase ("GB", TestName = "Validate API GB Country")]
        [Category("APIlist")] 
        public void ListCountryTest(string countryCode){
            RestClient restClient =  new RestClient("https://restcountries.eu/rest/v2");
            RestRequest restRequest = new RestRequest($"/alpha/{countryCode}", Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;
            // two validations just in case.
            Assert.IsNotNull(content);
            Assert.That(response.Content, Does.Contain(countryCode));
        }

        // 2 -  Try to get information for inexistent countries and validate the response
        [TestCase ("ZX", TestName = "Validate API ABC Country")]
        [Category("APIlistNonExistingCountries")] 
        public void ListNonExistsCountryTest(string countryCode){
            RestClient restClient =  new RestClient("https://restcountries.eu/rest/v2");
            RestRequest restRequest = new RestRequest($"/alpha/{countryCode}", Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            IRestResponse response = restClient.Execute(restRequest);
            //In case you want to validate again change AreEqual to AreNotEqual
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // 3 - This API has not a POST method at the moment, but it is being developed
        [TestCase ("GIAN", TestName = "Validate Post new country")]
        [Category("APIPostExample")]
        public void PostTesting(string countryCode){
            RestClient restClient =  new RestClient("https://restcountries.eu/rest/v2");
            
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Test Country");
            jObjectbody.Add("alpha2_Code", "TC");
            jObjectbody.Add("alhpa3_code", "TCY");
            
            // I added /register/ a fake POST URL
            RestRequest restRequest = new RestRequest($"/register/{countryCode}", Method.POST);
            restRequest.AddParameter("application/json",jObjectbody,ParameterType.RequestBody); 
            IRestResponse restResponse = restClient.Execute(restRequest);
            // This assert should works.
            Assert.AreEqual(HttpStatusCode.Accepted, restResponse.StatusCode );
        }

    }
}
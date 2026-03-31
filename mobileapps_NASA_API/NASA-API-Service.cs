using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobileapps_NASA_API
{
    public class NASA_API_Service
    {
        string API_KEY = "juOGg5Ffhzgf0pFdezVrDRHs60aAhgHOpvjv7zcK";
        string API_URL = "https://images-api.nasa.gov";

        HttpClient _client = new HttpClient();

        public async Task<List<Item>> GetNASAData(string SearchQuery)
        {
       

            string requestURL = $"{API_URL}/search?q={SearchQuery}&media_type=image";

            HttpRequestMessage request = new(HttpMethod.Get, requestURL);

            request.Headers.Add("api_key", $"{API_KEY}");

            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch. {response}");

            }

            string contentString = await response.Content.ReadAsStringAsync();

            Rootobject result = JsonConvert.DeserializeObject<Rootobject>(contentString);

            return result.collection.items?.ToList() ?? new List<Item>();




        }





    }
}

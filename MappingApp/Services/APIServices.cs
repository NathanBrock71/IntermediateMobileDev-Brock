using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text;

namespace MappingApp.Services
{
    public class APIServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public APIServices()
        {
            _httpClient = new HttpClient();
            _apiKey = "AIzaSyB_8BH_A8GJHq-MwBqMfuxyVzDXihao5C0"; // Replace with your actual API key
        }

        public async Task<string> FetchDirectionsAsync(Location origin, Location destination)
        {
            var url = "https://routes.googleapis.com/directions/v2:computeRoutes";

            var requestBody = new
            {
                origin = new
                {
                    location = new
                    {
                        latLng = new
                        {
                            latitude = origin.Latitude,
                            longitude = origin.Longitude
                        }
                    }
                },
                destination = new
                {
                    location = new
                    {
                        latLng = new
                        {
                            latitude = destination.Latitude,
                            longitude = destination.Longitude
                        }
                    }
                },
                travelMode = "DRIVE",
                routingPreference = "TRAFFIC_AWARE",
                departureTime = "2023-10-15T15:01:23.045123456Z",
                computeAlternativeRoutes = false,
                routeModifiers = new
                {
                    avoidTolls = false,
                    avoidHighways = false,
                    avoidFerries = false
                },
                languageCode = "en-US",
                units = "IMPERIAL"
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.Add("X-Goog-Api-Key", _apiKey);
            content.Headers.Add("X-Goog-FieldMask", "routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseContent);

            if (responseObject["routes"] != null && responseObject["routes"].Any())
            {
                return responseObject["routes"][0]["polyline"]["encodedPolyline"].ToString();
            }

            return null;
        }
    }
}
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

            var departureTime = DateTime.UtcNow.AddMinutes(5).ToString("yyyy-MM-ddTHH:mm:ssZ");

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
                departureTime = departureTime,
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
            var cont = await response.Content.ReadAsStringAsync();
            var req = $"Request URL: {url}" + $"Request Body: {json}";
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseContent);

            if (responseObject["routes"] != null && responseObject["routes"].Any())
            {
                return responseObject["routes"][0]["polyline"]["encodedPolyline"].ToString();
            }

            return null;
        }

        public List<Location> DecodePolyline(string encodedPolyline)
        {
            if (string.IsNullOrEmpty(encodedPolyline))
                return null;

            var polyline = new List<Location>();
            var polylineChars = encodedPolyline.ToCharArray();
            var index = 0;
            var currentLat = 0;
            var currentLng = 0;

            while (index < polylineChars.Length)
            {
                // Decode latitude
                var sum = 0;
                var shifter = 0;
                int nextFiveBits;

                do
                {
                    nextFiveBits = polylineChars[index++] - 63;
                    sum |= (nextFiveBits & 31) << shifter;
                    shifter += 5;
                } while (nextFiveBits >= 32 && index < polylineChars.Length);

                if ((sum & 1) != 0)
                    currentLat += ~(sum >> 1);
                else
                    currentLat += (sum >> 1);

                // Decode longitude
                sum = 0;
                shifter = 0;

                do
                {
                    nextFiveBits = polylineChars[index++] - 63;
                    sum |= (nextFiveBits & 31) << shifter;
                    shifter += 5;
                } while (nextFiveBits >= 32 && index < polylineChars.Length);

                if ((sum & 1) != 0)
                    currentLng += ~(sum >> 1);
                else
                    currentLng += (sum >> 1);

                var lat = currentLat / 1E5;
                var lng = currentLng / 1E5;
                polyline.Add(new Location(lat, lng));
            }

            return polyline;
        }
    }
}
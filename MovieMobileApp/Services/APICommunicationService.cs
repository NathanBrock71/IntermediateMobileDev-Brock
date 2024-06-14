using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMobileApp.Services
{
    public class APICommunicationService
    {
        private readonly HttpClient _httpClient;

        public APICommunicationService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://302a-2600-6c44-7af0-8b10-d94d-c9ec-2587-88a.ngrok-free.app/api/");
        }

        public async Task<string> GetAllMoviesAsync()
        {
            var response = await _httpClient.GetAsync("Movie");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetMovieByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"Movie/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateMovieAsync(string id, string title, string description, string director, string rating, string releaseDate, string reviewScore, string posterUrl)
        {
            var movie = new
            {
                id,
                title,
                description,
                director,
                rating,
                releaseDate,
                reviewScore,
                posterUrl
            };

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(movie), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Movie", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateMovieAsync(string id, string title, string description, string director, string rating, string releaseDate, string reviewScore, string posterUrl)
        {
            var movie = new
            {
                id,
                title,
                description,
                director,
                rating,
                releaseDate,
                reviewScore,
                posterUrl
            };

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(movie), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Movie/{id}", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteMovieAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"Movie/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

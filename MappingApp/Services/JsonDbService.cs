using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MappingApp.Models;

namespace MappingApp.Services
{
    public class JsonDbService
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "DeviceRoutes.json");

        public JsonDbService() { }

        public async Task AddRouteAsync(Route newRoute)
        {
            try
            {
                var routesList = await GetRoutesAsync() ?? new List<Route>();
                routesList.Add(newRoute);

                // Serialize the route list to a JSON string
                string jsonString = JsonSerializer.Serialize(routesList, new JsonSerializerOptions { WriteIndented = true });

                // Save the JSON string to a file
                await File.WriteAllTextAsync(filePath, jsonString);

                Console.WriteLine("Route saved successfully.");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task<List<Route>> GetRoutesAsync()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = await File.ReadAllTextAsync(filePath);
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        return JsonSerializer.Deserialize<List<Route>>(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            return new List<Route>();
        }

        public async Task RemoveRouteAsync(Route routeToRemove)
        {
            try
            {
                var routesList = await GetRoutesAsync();
                if (routesList != null)
                {
                    // Find the route with matching start point, end point, and name
                    var route = routesList.FirstOrDefault(r =>
                        r.StartPoint.Latitude == routeToRemove.StartPoint.Latitude &&
                        r.StartPoint.Longitude == routeToRemove.StartPoint.Longitude &&
                        r.EndPoint.Latitude == routeToRemove.EndPoint.Latitude &&
                        r.EndPoint.Longitude == routeToRemove.EndPoint.Longitude &&
                        r.Name == routeToRemove.Name);

                    if (route != null)
                    {
                        routesList.Remove(route);

                        // Serialize the updated route list to a JSON string
                        string jsonString = JsonSerializer.Serialize(routesList, new JsonSerializerOptions { WriteIndented = true });

                        // Save the JSON string to a file
                        await File.WriteAllTextAsync(filePath, jsonString);

                        Console.WriteLine("Route removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Route not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

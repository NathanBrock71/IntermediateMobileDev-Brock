using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MappingApp.Models;

namespace MappingApp.ViewModels
{
    public class CreateRoutePageViewModel
    {
        private string _startLocation;
        private string _endLocation;
        public event PropertyChangedEventHandler PropertyChanged;

        public string StartLocation
        {
            get { return _startLocation; }
            set { SetProperty(ref _startLocation, value); }
        }

        public string EndLocation
        {
            get => _endLocation;
            set => SetProperty(ref _endLocation, value);
        }

        public ICommand CreateRouteCommand { get; }

        public CreateRoutePageViewModel()
        {
            CreateRouteCommand = new Command(async () => await CreateRoute());
        }

        private async Task CreateRoute()
        {
            if (string.IsNullOrWhiteSpace(StartLocation) || string.IsNullOrWhiteSpace(EndLocation))
            {
                Console.WriteLine("Both start and end locations must be provided.");
                await App.Current.MainPage.DisplayAlert("Error", "Both start and end locations must be provided.", "OK");
                return;
            }

            try
            {
                // Create the route
                Models.Route newRoute = new Models.Route(await CreateLocation(StartLocation), await CreateLocation(EndLocation));

                // Serialize the route to a JSON string
                string jsonString = JsonSerializer.Serialize(newRoute, new JsonSerializerOptions { WriteIndented = true });

                // Define the file path
                string filePath = Path.Combine(FileSystem.AppDataDirectory, "routes.json");

                // Save the JSON string to a file
                await File.WriteAllTextAsync(filePath, jsonString);

                Console.WriteLine("Route saved successfully.");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            // Find out direction/driving routes display
        }

        public async Task<Location> CreateLocation(string locationString)
        {
            // Regex pattern to match coordinates (latitude and longitude)
            string coordinatePattern = @"^([-+]?\d{1,2}\.\d+),\s*([-+]?\d{1,3}\.\d+)$";
            var coordinateMatch = Regex.Match(locationString, coordinatePattern);

            if (coordinateMatch.Success)
            {
                // The string is in coordinate format
                double latitude = double.Parse(coordinateMatch.Groups[1].Value, CultureInfo.InvariantCulture);
                double longitude = double.Parse(coordinateMatch.Groups[2].Value, CultureInfo.InvariantCulture);
                return new Location(latitude, longitude);
            }
            else
            {
                // The string is an address, use geocoding to get the location
                IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(locationString);
                Location location = locations?.FirstOrDefault();

                if (location == null)
                {
                    throw new Exception("No location found for the provided address.");
                }

                return location;
            }
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

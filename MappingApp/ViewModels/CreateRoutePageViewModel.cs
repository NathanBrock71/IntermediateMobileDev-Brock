using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MappingApp.Models;
using MappingApp.Services;

namespace MappingApp.ViewModels
{
    public class CreateRoutePageViewModel : BaseViewModel
    {
        private string _startLocation;
        private string _endLocation;
        private string _routeName;

        public string StartLocation
        {
            get { return _startLocation; }
            set { SetProperty(ref _startLocation, value); }
        }

        public string EndLocation
        {
            get { return _endLocation; }
            set { SetProperty(ref _endLocation, value); }
        }

        public string RouteName
        {
            get { return _routeName; }
            set { SetProperty(ref _routeName, value); }
        }

        public ICommand CreateRouteCommand { get; }

        public CreateRoutePageViewModel()
        {
            CreateRouteCommand = new Command(async () => await CreateRoute());
        }

        private async Task CreateRoute()
        {
            if (string.IsNullOrWhiteSpace(StartLocation) || string.IsNullOrWhiteSpace(EndLocation) || string.IsNullOrWhiteSpace(RouteName))
            {
                Console.WriteLine("Name, start and end locations must be provided.");
                await App.Current.MainPage.DisplayAlert("Error", "Name, start and end locations must be provided.", "OK");
                return;
            }

            try
            {
                // Create the route
                Route newRoute = new Route(await CreateLocation(StartLocation), await CreateLocation(EndLocation), RouteName);

                await _dbService.AddRouteAsync(newRoute);


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            
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
    }
}

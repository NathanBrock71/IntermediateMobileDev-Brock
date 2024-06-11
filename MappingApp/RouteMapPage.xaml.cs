using MappingApp.Models;
using MappingApp.Services;
using MappingApp.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace MappingApp;

public partial class RouteMapPage : ContentPage
{
    private readonly APIServices _apiService;

    public RouteMapPage(Route route)
    {
        InitializeComponent();
        _apiService = new APIServices();
        BindingContext = new RouteMapPageViewModel(route);
        ShowRoute(route);
    }

    private async void ShowRoute(Models.Route route)
    {
        var startPin = new Pin
        {
            Label = "Start Location",
            Type = PinType.Place,
            Location = route.StartPoint
        };

        var endPin = new Pin
        {
            Label = "End Location",
            Type = PinType.Place,
            Location = route.EndPoint
        };

        routeMap.Pins.Add(startPin);
        routeMap.Pins.Add(endPin);

        // Fetch and display the route polyline
        var polyline = await _apiService.FetchDirectionsAsync(route.StartPoint, route.EndPoint);
        if (!string.IsNullOrEmpty(polyline))
        {
            var locations = _apiService.DecodePolyline(polyline);
            if (locations != null)
            {
                var mapPolyline = new Polyline
                {
                    StrokeColor = Colors.Blue,
                    StrokeWidth = 5
                };

                foreach (var location in locations)
                {
                    mapPolyline.Geopath.Add(location);
                }

                routeMap.MapElements.Add(mapPolyline);

                // Calculate the bounding box of the polyline and pins
                double minLat = Math.Min(route.StartPoint.Latitude, route.EndPoint.Latitude);
                double maxLat = Math.Max(route.StartPoint.Latitude, route.EndPoint.Latitude);
                double minLng = Math.Min(route.StartPoint.Longitude, route.EndPoint.Longitude);
                double maxLng = Math.Max(route.StartPoint.Longitude, route.EndPoint.Longitude);

                foreach (var location in locations)
                {
                    minLat = Math.Min(minLat, location.Latitude);
                    maxLat = Math.Max(maxLat, location.Latitude);
                    minLng = Math.Min(minLng, location.Longitude);
                    maxLng = Math.Max(maxLng, location.Longitude);
                }

                // Center the map on the bounding box
                var centerLat = (minLat + maxLat) / 2;
                var centerLng = (minLng + maxLng) / 2;
                var distanceLat = maxLat - minLat;
                var distanceLng = maxLng - minLng;

                // Set the zoom level based on the distance between the bounding box edges
                var zoom = Math.Max(distanceLat, distanceLng) * 50; // Adjust zoom level as needed

                var region = MapSpan.FromCenterAndRadius(
                new Location(centerLat, centerLng),
                Distance.FromMiles(zoom));

                routeMap.MoveToRegion(region);
            }
        }

        // Fetch distance and duration and set labels
        var distance = await _apiService.FetchDistanceAsync(route.StartPoint, route.EndPoint);
        var duration = await _apiService.FetchDurationAsync(route.StartPoint, route.EndPoint);

        // Set the text for the labels
        distanceLabel.Text = $"{distance:F1} miles";
        durationLabel.Text = $"{duration.Item1} hours {duration.Item2} minutes";

    }
}
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


        var region = MapSpan.FromCenterAndRadius(
            new Location((route.StartPoint.Latitude + route.EndPoint.Latitude) / 2,
                         (route.StartPoint.Longitude + route.EndPoint.Longitude) / 2),
            Distance.FromMiles(1));

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
            }
        }

        routeMap.MoveToRegion(region);


    }
}
using MappingApp.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace MappingApp;

public partial class RouteMapPage : ContentPage
{
    public RouteMapPage(Route route)
    {
        InitializeComponent();
        ShowRoute(route);
    }

    private void ShowRoute(Models.Route route)
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

        routeMap.MoveToRegion(region);
    }
}
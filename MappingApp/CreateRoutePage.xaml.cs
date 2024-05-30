using MappingApp.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace MappingApp;

public partial class CreateRoutePage : ContentPage
{
    public CreateRoutePage()
    {
        InitializeComponent();
        BindingContext = new CreateRoutePageViewModel(); ;
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        // Example function to handle the map click
        AddPin(e.Location);
    }

    private async void AddLocationFromPin(object sender, EventArgs e)
    {
        if (routeMap.Pins.Count == 0)
        {
            await DisplayAlert("Warning", "No pin found", "OK");
            return;
        }

        var pin = routeMap.Pins.First();
        var button = sender as Button;

        if (button == startPinButton)
        {
            startLocationEntry.Text = $"{pin.Location.Latitude}, {pin.Location.Longitude}";
        }
        else if (button == endPinButton)
        {
            endLocationEntry.Text = $"{pin.Location.Latitude}, {pin.Location.Longitude}";
        }
    }

    private async void SetPinToLocation(object sender, EventArgs e)
    {
        var entry = sender as Entry;
        if (entry == null) return;

        string address = entry.Text;
        IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);

        Location location = locations?.FirstOrDefault();

        if (location == null)
        {
            await DisplayAlert("Warning", "No location found", "OK");
            return;
        }

        AddPin(location);
        routeMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude), Distance.FromMiles(1)));
    }

    private void AddPin(Location location)
    {
        routeMap.Pins.Clear();

        var pin = new Pin
        {
            Label = "temp",
            Type = PinType.Place,
            Location = new Location(location.Latitude, location.Longitude)
        };

        pin.MarkerClicked += Pin_MarkerClicked;
        routeMap.Pins.Add(pin);
    }

    private void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
    {
        var pin = (Pin)sender;
        routeMap.Pins.Remove(pin);
    }
}
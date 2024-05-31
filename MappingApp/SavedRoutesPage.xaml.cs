using MappingApp.Models;
using MappingApp.ViewModels;

namespace MappingApp;

public partial class SavedRoutesPage : ContentPage
{
    public SavedRoutesPage()
    {
        InitializeComponent();
    }

    private async void OnRouteSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var selectedRoute = e.CurrentSelection[0] as Route;
            if (selectedRoute != null)
            {
                await Navigation.PushAsync(new RouteMapPage(selectedRoute));
            }
        }
    }
}
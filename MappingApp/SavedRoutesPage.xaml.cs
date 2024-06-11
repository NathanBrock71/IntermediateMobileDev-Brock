using MappingApp.Models;
using MappingApp.ViewModels;

namespace MappingApp;

public partial class SavedRoutesPage : ContentPage
{
    private SavedRoutesPageViewModel viewModel;

    public SavedRoutesPage()
    {
        InitializeComponent();
        viewModel = new SavedRoutesPageViewModel();
        BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadRoutesAsync(); // Load routes every time the page appears
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
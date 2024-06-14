using AndroidX.Lifecycle;
using Microsoft.Maui.Controls;
using MovieMobileApp.Model;
using MovieMobileApp.ViewModels;

namespace MovieMobileApp.Views;

public partial class MovieList : ContentPage
{
    private readonly MovieListViewModel _viewModel;

    public MovieList()
    {
        InitializeComponent();
        _viewModel = new MovieListViewModel();
        BindingContext = _viewModel;
        // Get the screen height in device-independent units (DIPs)
        var displayInfo = DeviceDisplay.MainDisplayInfo;
        var screenHeight = displayInfo.Height / displayInfo.Density;

        // Set the height of the CollectionView
        collectionView.HeightRequest = screenHeight;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.FetchMovies();
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var selectedMovie = e.CurrentSelection[0] as Movie;
            var detailPage = new MovieDetails();
            var detailViewModel = detailPage.BindingContext as MovieDetailsViewModel;
            detailViewModel.Initialize(selectedMovie);

            await Navigation.PushAsync(detailPage);

            // Clear the selection
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
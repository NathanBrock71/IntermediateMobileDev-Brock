using MappingApp.Models;
using MappingApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MappingApp.ViewModels
{
    public class SavedRoutesPageViewModel : BaseViewModel
    {
        private ObservableCollection<Route> routes;

        public ObservableCollection<Route> Routes
        {
            get { return routes; }
            set { SetProperty(ref routes, value); }
        }
        public ICommand ViewRouteCommand { get; }

        public SavedRoutesPageViewModel()
        {
            Routes = new ObservableCollection<Route>();
            ViewRouteCommand = new Command<Route>(async (route) => await ViewRoute(route));
            LoadRoutesAsync(); // Load routes when the view model is initialized
        }

        public async Task LoadRoutesAsync()
        {
            try
            {
                var loadedRoutes = await _dbService.GetRoutesAsync();
                Routes.Clear();
                foreach (var route in loadedRoutes)
                {
                    Routes.Add(route);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task ViewRoute(Route route)
        {
            // Implement navigation to the route details or map page
        }
    }
}

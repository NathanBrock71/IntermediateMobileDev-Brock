using MappingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MappingApp.ViewModels
{
    public class RouteMapPageViewModel : BaseViewModel
    {
        public Route CurrentRoute { get; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public ICommand DeleteRouteCommand { get; }

        public RouteMapPageViewModel(Route route)
        {
            CurrentRoute = route;
            DeleteRouteCommand = new Command(async () => await DeleteRoute());
        }
        public RouteMapPageViewModel()
        {
            DeleteRouteCommand = new Command(async () => await DeleteRoute());
        }

        public async Task DeleteRoute()
        {
            if (CurrentRoute != null)
            {
                await _dbService.RemoveRouteAsync(CurrentRoute);
                // Navigate to SavedRoutesPage
                await Shell.Current.GoToAsync("///SavedRoutesPage");
            }
        }

        public async Task<string> GetDirections(Route route)
        {
            try
            {
                var directions = await _apiService.FetchDirectionsAsync(route.StartPoint, route.EndPoint);
                return directions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}

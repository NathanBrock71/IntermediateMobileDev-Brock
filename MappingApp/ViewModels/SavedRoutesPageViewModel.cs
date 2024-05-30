using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MappingApp.ViewModels
{
    public class SavedRoutesPageViewModel
    {
        public ObservableCollection<Route> SavedRoutes { get; set; }
        public ICommand ViewRouteCommand { get; }

        public SavedRoutesPageViewModel()
        {
            SavedRoutes = new ObservableCollection<Route>();
            ViewRouteCommand = new Command<Route>(async (route) =>
            {
                // Logic to view route using Google Maps API
            });
        }
    }

    public class Route
    {
        public string RouteName { get; set; }
    }
}

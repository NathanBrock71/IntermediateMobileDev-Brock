using MappingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MappingApp.ViewModels
{
    public class SavedRoutesPageViewModel
    {
        public ObservableCollection<Route> Routes { get; set; } = new ObservableCollection<Route>();
        public ICommand ViewRouteCommand { get; }

        public SavedRoutesPageViewModel()
        {
            LoadRoutes();
        }
    }

    private async Task LoadRoutes()
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "routes.json");

        if (File.Exists(filePath))
        {
            string jsonString = await File.ReadAllTextAsync(filePath);
            var routes = JsonSerializer.Deserialize<List<Route>>(jsonString);
            if (routes != null)
            {
                foreach (var route in routes)
                {
                    Routes.Add(route);
                }
            }
        }
    }
}

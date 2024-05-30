using MappingApp.Services;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MappingApp.ViewModels
{
    public class CreateRoutePageViewModel
    {
        public readonly APIServices _apiServices;
        public ICommand CreateRouteCommand { get; }

        public CreateRoutePageViewModel(APIServices apiServices)
        {
            _apiServices = apiServices;
            CreateRouteCommand = new Command(async () => await CreateRoute());
        }

        private async Task CreateRoute()
        {
            // Logic to create route using Google Maps API
            // Use Google Maps Directions API to get route details and plot on the map
            
        }
    }
}

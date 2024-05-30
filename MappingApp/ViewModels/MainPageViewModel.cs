using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MappingApp.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand NavigateToCreateRouteCommand { get; }
        public ICommand NavigateToSavedRoutesCommand { get; }

        public MainPageViewModel()
        {
            NavigateToCreateRouteCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(CreateRoutePage)));
            NavigateToSavedRoutesCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(SavedRoutesPage)));
        }
    }
}
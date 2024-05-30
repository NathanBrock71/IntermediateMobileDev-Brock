using MappingApp.Services;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MappingApp.ViewModels
{
    public class CreateRoutePageViewModel
    {
        private string _startLocation;
        private string _endLocation;
        public event PropertyChangedEventHandler PropertyChanged;

        public string StartLocation
        {
            get { return _startLocation; }
            set { SetProperty(ref _startLocation, value); }
        }

        public string EndLocation
        {
            get => _endLocation;
            set => SetProperty(ref _endLocation, value);
        }

        public ICommand CreateRouteCommand { get; }

        public CreateRoutePageViewModel()
        {
            CreateRouteCommand = new Command(async () => await CreateRoute());
        }

        private async Task CreateRoute()
        {
            if (string.IsNullOrWhiteSpace(StartLocation) || string.IsNullOrWhiteSpace(EndLocation))
            {
                Console.WriteLine("Both start and end locations must be provided.");
                await App.Current.MainPage.DisplayAlert("Error", "Both start and end locations must be provided.", "OK");
                return;
            }

            Console.WriteLine($"Start Location: {StartLocation}");
            Console.WriteLine($"End Location: {EndLocation}");

            // Maybe move some code to VM from code behind
            // save a route
            // save route to temp db Json
            // Find out direction/driving routes display
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

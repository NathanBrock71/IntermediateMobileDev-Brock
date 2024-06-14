using MovieMobileApp.Model;
using MovieMobileApp.Services;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieMobileApp.ViewModels
{
    public class MovieListViewModel : BindableObject
    {
        private readonly APICommunicationService _apiService;
        private ObservableCollection<Movie> _movies;

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddItemCommand { get; }

        public MovieListViewModel()
        {
            _apiService = new APICommunicationService();
            Movies = new ObservableCollection<Movie>();
            AddItemCommand = new Command(OnAddItem);
            FetchMovies();
        }

        private async void FetchMovies()
        {
            var movies = await _apiService.GetAllMoviesAsync();
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var movieList = System.Text.Json.JsonSerializer.Deserialize<List<Movie>>(movies, options);
            Movies = new ObservableCollection<Movie>(movieList);
        }

        private void OnAddItem()
        {
            // Logic for adding a new item (navigate to a new page, etc.)
        }
    }
}

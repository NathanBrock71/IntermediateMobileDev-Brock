using MovieMobileApp.Model;
using MovieMobileApp.Services;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieMobileApp.Views;

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
            AddItemCommand = new Command(OnAdd);
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

        private async void OnAdd()
        {
            var formPage = new AddMovie();
            await Application.Current.MainPage.Navigation.PushAsync(formPage);
        }
    }
}

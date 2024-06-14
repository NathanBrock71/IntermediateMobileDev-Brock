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
using CoffeeAppSpring2024inclass.ViewModels;

namespace MovieMobileApp.ViewModels
{
    public class MovieListViewModel : BaseViewModel
    {
        private ObservableCollection<Movie> _movies;

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set
            {
                SetProperty(ref _movies, value);
            }
        }

        public ICommand AddItemCommand { get; }

        public MovieListViewModel()
        {
            Movies = new ObservableCollection<Movie>();
            AddItemCommand = new Command(OnAdd);
            FetchMovies();
        }

        public async void FetchMovies()
        {
            var movies = await _apiServices.GetAllMoviesAsync();
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var movieList = System.Text.Json.JsonSerializer.Deserialize<List<Movie>>(movies, options);

            foreach(Movie m in movieList)
            {
                if (m.PosterUrl == null)
                {
                    m.PosterUrl = "https://www.reelviews.net/resources/img/default_poster.jpg";
                }
            }

            Movies = new ObservableCollection<Movie>(movieList);
        }

        private async void OnAdd()
        {
            var formPage = new AddMovie();
            await Application.Current.MainPage.Navigation.PushAsync(formPage);
        }
    }
}

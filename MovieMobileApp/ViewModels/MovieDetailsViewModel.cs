using CoffeeAppSpring2024inclass.ViewModels;
using MovieMobileApp.Model;
using MovieMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieMobileApp.ViewModels
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        private Movie _movie;

        public Movie Movie
        {
            get => _movie;
            set => SetProperty(ref _movie, value);
        }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MovieDetailsViewModel()
        {
            EditCommand = new Command(OnEdit);
            DeleteCommand = new Command(OnDelete);
        }

        private async void OnEdit()
        {
            var formPage = new AddMovie();
            var formViewModel = formPage.BindingContext as AddMovieViewModel;
            formViewModel.Title = Movie.Title;
            formViewModel.Description = Movie.Description;
            formViewModel.Director = Movie.Director;
            formViewModel.Rating = Movie.Rating;
            formViewModel.ReviewScore = Movie.ReviewScore.ToString();
            formViewModel.ReleaseDate = Movie.ReleaseDate.ToString();
            formViewModel.PosterUrl = Movie.PosterUrl;

            await Application.Current.MainPage.Navigation.PushAsync(formPage);
        }

        private async void OnDelete()
        {
            bool isConfirmed = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this movie?", "Yes", "No");
            if (isConfirmed)
            {
                await _apiServices.DeleteMovieAsync(Movie.Id.ToString());
                await Application.Current.MainPage.DisplayAlert("Deleted", "Movie has been deleted", "OK");
            }
        }

        public void Initialize(Movie movie)
        {
            Movie = movie;
        }
    }
}

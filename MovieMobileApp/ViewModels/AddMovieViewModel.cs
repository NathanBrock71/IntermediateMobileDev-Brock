using Android.Media;
using CoffeeAppSpring2024inclass.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieMobileApp.ViewModels
{
    public class AddMovieViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string? _id;
        private string _title;
        private string _description;
        private string _director;
        private string _rating;
        private string _reviewScore;
        private string _releaseDate;
        private string _posterUrl;

        private string _titleError;
        private string _descriptionError;
        private string _directorError;
        private string _reviewScoreError;
        private string _releaseDateError;
        private string _posterUrlError;

        public string Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                SetProperty(ref _description, value);
            }
        }

        public string Director
        {
            get => _director;
            set
            {
                SetProperty(ref _director, value);
            }
        }

        public string Rating
        {
            get => _rating;
            set
            {
                SetProperty(ref _rating, value);
            }
        }

        public string ReviewScore
        {
            get => _reviewScore;
            set
            {
                SetProperty(ref _reviewScore, value);
            }
        }

        public string ReleaseDate
        {
            get => _releaseDate;
            set
            {
                SetProperty(ref _releaseDate, value);
            }
        }

        public string PosterUrl
        {
            get => _posterUrl;
            set
            {
                SetProperty(ref _posterUrl, value);
            }
        }

        public string TitleError
        {
            get => _titleError;
            set
            {
                _titleError = value;
                OnPropertyChanged(nameof(TitleError));
                OnPropertyChanged(nameof(IsTitleErrorVisible));
            }
        }

        public bool IsTitleErrorVisible => !string.IsNullOrEmpty(TitleError);

        public string DescriptionError
        {
            get => _descriptionError;
            set
            {
                _descriptionError = value;
                OnPropertyChanged(nameof(DescriptionError));
                OnPropertyChanged(nameof(IsDescriptionErrorVisible));
            }
        }

        public bool IsDescriptionErrorVisible => !string.IsNullOrEmpty(DescriptionError);

        public string DirectorError
        {
            get => _directorError;
            set
            {
                _directorError = value;
                OnPropertyChanged(nameof(DirectorError));
                OnPropertyChanged(nameof(IsDirectorErrorVisible));
            }
        }

        public bool IsDirectorErrorVisible => !string.IsNullOrEmpty(DirectorError);

        public string ReviewScoreError
        {
            get => _reviewScoreError;
            set
            {
                _reviewScoreError = value;
                OnPropertyChanged(nameof(ReviewScoreError));
                OnPropertyChanged(nameof(IsReviewScoreErrorVisible));
            }
        }

        public bool IsReviewScoreErrorVisible => !string.IsNullOrEmpty(ReviewScoreError);

        public string ReleaseDateError
        {
            get => _releaseDateError;
            set
            {
                _releaseDateError = value;
                OnPropertyChanged(nameof(ReleaseDateError));
                OnPropertyChanged(nameof(IsReleaseDateErrorVisible));
            }
        }

        public bool IsReleaseDateErrorVisible => !string.IsNullOrEmpty(ReleaseDateError);

        public string PosterUrlError
        {
            get => _posterUrlError;
            set
            {
                _posterUrlError = value;
                OnPropertyChanged(nameof(PosterUrlError));
                OnPropertyChanged(nameof(IsPosterUrlErrorVisible));
            }
        }

        public bool IsPosterUrlErrorVisible => !string.IsNullOrEmpty(PosterUrlError);

        public ICommand SaveCommand { get; }

        public AddMovieViewModel()
        {
            SaveCommand = new Command(OnSave);
        }

        private async void OnSave()
        {
            if (Validate())
            {
                if (Id != null)
                {
                    await _apiServices.UpdateMovieAsync(Id, Title, Description, Director, Rating, ReleaseDate, ReviewScore, PosterUrl);
                } else
                {
                    await _apiServices.CreateMovieAsync(new Guid().ToString(), Title, Description, Director, Rating, ReleaseDate, ReviewScore, PosterUrl);
                }
            }
        }

        private bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Title) || Title.Length > 100)
            {
                TitleError = "Title is required and must be less than 100 characters.";
                isValid = false;
            }
            else
            {
                TitleError = string.Empty;
            }

            if (string.IsNullOrEmpty(Description) || Description.Length > 1000)
            {
                DescriptionError = "Description is required and must be less than 1000 characters.";
                isValid = false;
            }
            else
            {
                DescriptionError = string.Empty;
            }

            if (string.IsNullOrEmpty(Director) || Director.Length > 50)
            {
                DirectorError = "Director is required and must be less than 50 characters.";
                isValid = false;
            }
            else
            {
                DirectorError = string.Empty;
            }

            if (!double.TryParse(ReviewScore, out double reviewScoreValue) || reviewScoreValue < 0 || reviewScoreValue > 10)
            {
                ReviewScoreError = "Review score must be a number between 0 and 10.";
                isValid = false;
            }
            else
            {
                ReviewScoreError = string.Empty;
            }

            if (string.IsNullOrEmpty(ReleaseDate) || !Regex.IsMatch(ReleaseDate, @"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$"))
            {
                ReleaseDateError = "Release date must be in the format MM/DD/YYYY.";
                isValid = false;
            }
            else
            {
                ReleaseDateError = string.Empty;
            }

            if (!string.IsNullOrEmpty(PosterUrl) && PosterUrl.Length > 200)
            {
                PosterUrlError = "Poster URL must be less than 200 characters.";
                isValid = false;
            }
            else
            {
                PosterUrlError = string.Empty;
            }

            return isValid;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

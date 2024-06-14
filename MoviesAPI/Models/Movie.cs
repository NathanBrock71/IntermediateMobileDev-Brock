using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Movie Title is required")]
        [MaxLength(100, ErrorMessage = "Max Title Length is 100")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Movie Description is required")]
        [MaxLength(1000, ErrorMessage = "Max Description Length is 1000")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Movie Director is required")]
        [MaxLength(50, ErrorMessage = "Max Director Length is 50")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Movie Rating is required")]
        [MaxLength(6, ErrorMessage = "Max Rating Length is 6")]
        public string Rating { get; set; }

        [Required(ErrorMessage = "Movie ReleaseDate is required")]
        [MaxLength(18, ErrorMessage = "Max ReleaseDate Length is 18")]
        public string ReleaseDate { get; set; }

        [Required(ErrorMessage = "Movie ReviewScore is required")]
        [MaxLength(7, ErrorMessage = "Max ReviewScore Length is 7")]
        public string ReviewScore { get; set; }

        [MaxLength(200, ErrorMessage = "Max url lenght is 200")]
        public string PosterUrl { get; set; }

        public Movie() { }

        public Movie(string title, string description, string rating, string releaseDate, string reviewScore, string posterUrl)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Rating = rating;
            ReleaseDate = releaseDate;
            ReviewScore = reviewScore;
            PosterUrl = posterUrl;
        }
    }
}

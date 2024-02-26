namespace MoviesAPI.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public string? Rating { get; set; }
        public string? ReleaseDate { get; set; }
        public string? ReviewScore { get; set; }

        public Movie() { }

        public Movie(string title, string description, string rating, string releaseDate, string reviewScore)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Rating = rating;
            ReleaseDate = releaseDate;
            ReviewScore = reviewScore;
        }
    }
}

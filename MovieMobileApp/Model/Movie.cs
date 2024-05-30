using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMobileApp.Model
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public string Rating { get; set; }

        public string ReleaseDate { get; set; }

        public string ReviewScore { get; set; }
    }
}

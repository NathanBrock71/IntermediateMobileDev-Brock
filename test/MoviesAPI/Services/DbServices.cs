using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class DbServices
    {
        private readonly MoviesDBContext _context;

        public DbServices(MoviesDBContext context)
        {
            _context = context;
        }

        // Get all movies
        public List<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        // Get movie by ID
        public Movie GetMovieById(Guid id)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == id);
        }

        // Add a new movie
        public void AddMovie(Movie movie)
        {
            try
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Movie: " + ex.Message);
            }

        }

        // Update movie
        public void UpdateMovie(Movie movie)
        {
            try
            {
                _context.Movies.Update(movie);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Movie: " + ex.Message);
            }
        }

        // Delete movie by ID
        public void DeleteMovieById(Guid id)
        {
            var movieToDelete = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movieToDelete != null)
            {
                try
                {
                    _context.Movies.Remove(movieToDelete);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Deleting Movie: " + ex.Message);
                }

            }
        }
    }
}

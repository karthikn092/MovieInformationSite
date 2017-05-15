using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Logics
{
    public class MovieOperations
    {
        private readonly MovieDbContext _movieDbContext;

        public MovieOperations()
        {
            _movieDbContext = new MovieDbContext();
        }

        public void AddMovie(Movie movie)
        {
            _movieDbContext.Movie.Add(movie);
            _movieDbContext.SaveChanges();
        }

        public List<Movie> GetAllMovies()
        {
            return _movieDbContext.Movie.ToList();
        }

        public void DeleteMovie(Movie movie)
        {
            _movieDbContext.Movie.Remove(movie);
            _movieDbContext.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            Movie dest = _movieDbContext.Movie.SingleOrDefault(x => x.MovieId == movie.MovieId);
            dest.Name = movie.Name;
            dest.Plot = movie.Plot;
            dest.Poster = movie.Poster;
            dest.ProducerId = movie.ProducerId;
            dest.YearOfRelease = movie.YearOfRelease;

            _movieDbContext.SaveChanges();
        }

        public Movie FindMovieById(Guid id)
        {
            Movie movie = _movieDbContext.Movie.SingleOrDefault(item => item.MovieId == id);

            return movie;
        }
    }
}

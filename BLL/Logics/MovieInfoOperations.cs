using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Logics
{
    public class MovieInfoOperations
    {
        private readonly MovieDbContext _movieDbContext;

        public MovieInfoOperations()
        {
            _movieDbContext = new MovieDbContext();
        }

        public void AddMovieInfo(MovieInfo movieInfo)
        {
            _movieDbContext.MovieInfo.Add(movieInfo);
            _movieDbContext.SaveChanges();
        }

        public List<MovieInfo> GetAllMovieInfomation()
        {
            return _movieDbContext.MovieInfo.ToList();
        }

        public void DeleteMovieInfo(MovieInfo movieInfo)
        {
            _movieDbContext.MovieInfo.Remove(movieInfo);
            _movieDbContext.SaveChanges();
        }

        public void UpdateMovieInfo(MovieInfo movieInfo)
        {
            _movieDbContext.Entry(movieInfo).State = System.Data.Entity.EntityState.Modified;
            _movieDbContext.SaveChanges();
        }

        public List<MovieInfo> FindMovieInfoById(Guid id)
        {
            List<MovieInfo> MovieInfo = _movieDbContext.MovieInfo.Where(x => x.MovieId == id).ToList<MovieInfo>();

            return MovieInfo;
        }
    }
}

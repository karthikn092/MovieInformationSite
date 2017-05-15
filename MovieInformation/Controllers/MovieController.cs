using BLL.Logics;
using BLL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieInformation.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieOperations _movieOperations;
        private readonly ProducerOperations _producerOperations;
        private readonly ActorOperations _actorOperations;
        private readonly MovieInfoOperations _movieInfoOperations;
        private readonly Random _random;

        public MovieController() : base()
        {
            _movieOperations = new MovieOperations();
            _producerOperations = new ProducerOperations();
            _actorOperations = new ActorOperations();
            _movieInfoOperations = new MovieInfoOperations();
            _random = new Random();
        }

        // GET: Movie
        public ActionResult Index()
        {
            List<MovieModel> movieModelList = new List<MovieModel>();
            List<Movie> movieList = _movieOperations.GetAllMovies();

            foreach (var movie in movieList)
            {
                MovieModel movieModel = new MovieModel();
                movieModel.Id = movie.MovieId;
                movieModel.Name = movie.Name;
                movieModel.Plot = movie.Plot;
                movieModel.Poster = movie.Poster;
                movieModel.Producer = _producerOperations.FindProducerById(movie.ProducerId).Name;
                movieModel.YearOfRelease = movie.YearOfRelease;

                List<MovieInfo> movieInfo = _movieInfoOperations.GetAllMovieInfomation();
                List<Guid> actorIds = movieInfo.FindAll(x => x.MovieId == movie.MovieId).Select(id => id.ActorId).ToList();

                movieModel.Actors = new List<string>();
                foreach (var id in actorIds)
                {
                    movieModel.Actors.Add(_actorOperations.FindActorById(id).Name);
                }
                movieModel.ActorList = movieModel != null ? string.Join(",", movieModel.Actors) : "";
                movieModelList.Add(movieModel);
            }
            return View(movieModelList);
        }

        // GET: Movie/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            MovieModel movieModel = new MovieModel();
            return View(movieModel);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieModel movieModel)
        {
            if (movieModel != null)
            {
                Guid producerId = Guid.Empty;
                Guid movieId = Guid.NewGuid();

                if (!string.IsNullOrEmpty(movieModel.Producer))
                {
                    Producer producer = _producerOperations.FindProducerByName(movieModel.Producer);
                    if (producer != null)
                    {
                        producerId = producer.ProducerId;
                    }
                    else
                    {
                        _producerOperations.AddProducer(new Producer() { ProducerId = producerId = Guid.NewGuid(), Name = movieModel.Producer });
                    }
                }

                Movie movie = new Movie()
                {
                    MovieId = movieId,
                    Name = movieModel.Name,
                    Plot = movieModel.Plot,
                    YearOfRelease = movieModel.YearOfRelease,
                    ProducerId = producerId,
                    Poster = movieModel.Poster
                };
                _movieOperations.AddMovie(movie);

                if (!string.IsNullOrEmpty(movieModel.ActorList))
                {
                    List<string> actorList = movieModel.ActorList.Split(',').ToList<string>();
                    foreach (var actor in actorList)
                    {
                        Guid actorId = Guid.Empty;
                        Actor actorModel = _actorOperations.FindActorByName(actor);
                        if (actorModel == null)
                        {
                            _actorOperations.AddActor(new Actor() { ActorId = actorId = Guid.NewGuid(), Name = actor });
                        }
                        else
                        {
                            actorId = actorModel.ActorId;
                        }
                        _movieInfoOperations.AddMovieInfo(new MovieInfo() { Id = Guid.NewGuid(), ActorId = actorId, MovieId = movieId });
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(Guid id)
        {
            MovieModel model = new MovieModel();
            Movie movie = _movieOperations.FindMovieById(id);
            model.Name = movie.Name;
            model.Poster = movie.Poster;
            model.Plot = movie.Plot;
            model.YearOfRelease = movie.YearOfRelease;
            model.Producer = _producerOperations.FindProducerById(movie.ProducerId).Name;

            List<MovieInfo> movieInfo = _movieInfoOperations.GetAllMovieInfomation();
            List<Guid> actorIds = movieInfo.FindAll(x => x.MovieId == movie.MovieId).Select(item => item.ActorId).ToList();

            model.Actors = new List<string>();
            foreach (var ac in actorIds)
            {
                model.Actors.Add(_actorOperations.FindActorById(ac).Name);
            }
            model.ActorList = model != null ? string.Join(",", model.Actors) : "";

            return View(model);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, MovieModel model)
        {
            try
            {
                Movie movie = new Movie();
                movie.MovieId = id;
                movie.Name = model.Name;
                movie.Plot = model.Plot;
                movie.Poster = model.Poster;
                movie.YearOfRelease = model.YearOfRelease;
                Guid producerId = Guid.Empty;

                if (!string.IsNullOrEmpty(model.Producer))
                {
                    Producer producer = _producerOperations.FindProducerByName(model.Producer);
                    if (producer != null)
                    {
                        producerId = producer.ProducerId;
                    }
                    else
                    {
                        _producerOperations.AddProducer(new Producer() { ProducerId = producerId = Guid.NewGuid(), Name = model.Producer });
                    }
                }

                    movie.ProducerId = producerId;
                    _movieOperations.UpdateMovie(movie);

                if (!string.IsNullOrEmpty(model.ActorList))
                {
                    List<string> actorList = model.ActorList.Split(',').ToList<string>();
                    List<string> actorData = FindActors(model.Id);
                    foreach (var actor in actorList)
                    {
                        Guid actorId = Guid.Empty;

                        if(!actorData.Contains(actor))
                        {
                            Actor actorModel = _actorOperations.FindActorByName(actor);
                            if (actorModel == null)
                            {
                                _actorOperations.AddActor(new Actor() { ActorId = actorId = Guid.NewGuid(), Name = actor });
                                _movieInfoOperations.AddMovieInfo(new MovieInfo() { Id = Guid.NewGuid(), ActorId = actorId, MovieId = model.Id });

                            }
                        }
                    }
                    foreach(var actor in actorData)
                    {
                        if(!actorList.Contains(actor))
                        {
                            Actor actorModel = _actorOperations.FindActorByName(actor);
                            if (actorModel != null)
                            {
                                MovieInfo movieInfo = _movieInfoOperations.FindMovieInfoById(model.Id)?.FirstOrDefault(x => x.ActorId == actorModel.ActorId);

                                _movieInfoOperations.DeleteMovieInfo(movieInfo);

                            }
                        }
                    }
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(Guid id)
        {
            MovieModel model = new MovieModel();
            Movie movie = _movieOperations.FindMovieById(id);
            model.Name = movie.Name;
            model.Poster = movie.Poster;
            model.Plot = movie.Plot;
            model.YearOfRelease = movie.YearOfRelease;
            model.Producer = _producerOperations.FindProducerById(movie.ProducerId).Name;

            List<MovieInfo> movieInfo = _movieInfoOperations.GetAllMovieInfomation();
            List<Guid> actorIds = movieInfo.FindAll(x => x.MovieId == movie.MovieId).Select(item => item.ActorId).ToList();

            model.Actors = new List<string>();
            foreach (var ac in actorIds)
            {
                model.Actors.Add(_actorOperations.FindActorById(ac).Name);
            }
            model.ActorList = model != null ? string.Join(",", model.Actors) : "";
            return View(model);
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, MovieModel model)
        {
            try
            {
                Movie movie = _movieOperations.FindMovieById(id);
                if(movie!=null)
                {
                    _movieOperations.DeleteMovie(movie);

                    List<MovieInfo> movieInfo = _movieInfoOperations.FindMovieInfoById(id);
                    foreach(var info in movieInfo)
                    {
                        _movieInfoOperations.DeleteMovieInfo(info);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<string> FindActors(Guid id) 
        {
            List<MovieInfo> movieInfo = _movieInfoOperations.FindMovieInfoById(id);
            List<string> actors = new List<string>();

            foreach(var movie in movieInfo)
            {
                Actor actor = _actorOperations.FindActorById(movie.ActorId);
                if(actor!=null)
                {
                    actors.Add(actor.Name);
                }
            }
            return actors;

        }
    }
}

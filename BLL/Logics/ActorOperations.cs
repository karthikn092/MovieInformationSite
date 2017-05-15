using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Logics
{
    public class ActorOperations
    {
        private readonly MovieDbContext _movieDbContext;

        public ActorOperations()
        {
            _movieDbContext = new MovieDbContext();
        }

        public void AddActor(Actor actor)
        {
            _movieDbContext.Actor.Add(actor);
            _movieDbContext.SaveChanges();
        }

        public List<Actor> GetAllActors()
        {
            return _movieDbContext.Actor.ToList();
        }

        public void DeleteActor(Actor actor)
        {
            _movieDbContext.Actor.Remove(actor);
            _movieDbContext.SaveChanges();
        }

        public void UpdateActor(Actor actor)
        {
            _movieDbContext.Entry(actor).State = System.Data.Entity.EntityState.Modified;
            _movieDbContext.SaveChanges();
        }

        public Actor FindActorById(Guid id)
        {
            Actor actor = _movieDbContext.Actor.SingleOrDefault(item => item.ActorId == id);

            return actor;
        }

        public Actor FindActorByName(string name)
        {
            Actor actor = _movieDbContext.Actor.SingleOrDefault(item => string.Compare(item.Name, name, StringComparison.OrdinalIgnoreCase) == 0);

            return actor;
        }
    }
}

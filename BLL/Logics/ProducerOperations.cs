using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Logics
{
    public class ProducerOperations
    {
        private readonly MovieDbContext _movieDbContext;

        public ProducerOperations()
        {
            _movieDbContext = new MovieDbContext();
        }

        public void AddProducer(Producer producer)
        {
            _movieDbContext.Producer.Add(producer);
            _movieDbContext.SaveChanges();
        }

        public List<Producer> GetAllProducers()
        {
            return _movieDbContext.Producer.ToList();
        }

        public void DeleteProducer(Producer producer)
        {
            _movieDbContext.Producer.Remove(producer);
            _movieDbContext.SaveChanges();
        }

        public void UpdateProducer(Producer producer)
        {
            Producer dest = _movieDbContext.Producer.SingleOrDefault(x => x.ProducerId == producer.ProducerId);

            dest.Bio=producer.Bio;
            dest.Name=producer.Name;
            dest.Dob=producer.Dob;

            _movieDbContext.SaveChanges();
        }

        public Producer FindProducerById(Guid id)
        {
            Producer producer = _movieDbContext.Producer.SingleOrDefault(item => item.ProducerId == id);

            return producer;
        }

        public Producer FindProducerByName(string name)
        {
            Producer producer = _movieDbContext.Producer.SingleOrDefault(item => string.Compare(item.Name, name, StringComparison.OrdinalIgnoreCase) == 0);

            return producer;
        }
    }
}

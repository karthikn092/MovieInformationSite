using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Movie
    {
        public Guid MovieId { get; set; }

        public string Name { get; set; }

        public string YearOfRelease { get; set; }

        public string Plot { get; set; }

        public byte[] Poster { get; set; }

        [ForeignKey("Producer")]
        public Guid ProducerId { get; set; }

        public Producer Producer { get; set; }
    }
}

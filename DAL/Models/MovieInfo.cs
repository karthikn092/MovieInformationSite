using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class MovieInfo
    {
        public Guid Id { get; set; }

        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey("Actor")]
        public Guid ActorId { get; set; }

        public Actor Actor { get; set; }
    }
}

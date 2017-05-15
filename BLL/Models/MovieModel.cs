
using System;
using System.Collections.Generic;

namespace BLL.Models
{
    public class MovieModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string YearOfRelease { get; set; }

        public string Plot { get; set; }

        public byte[] Poster { get; set; }

        public string Producer { get; set; }

        public List<string> Actors { get; set; }

        public string ActorList { get; set; }
    }
}

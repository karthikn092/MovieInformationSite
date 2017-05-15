using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Producer
    {
        public Guid ProducerId { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public string Bio { get; set; }
    }
}

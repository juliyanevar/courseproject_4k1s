using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class AuditoriumType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Auditorium> Auditoriums { get; set; }
    }
}

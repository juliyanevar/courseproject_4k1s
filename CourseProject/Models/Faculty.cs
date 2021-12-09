using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class Faculty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Pulpit> Pulpits { get; set; }
        public ICollection<Profession> Professions { get; set; }
    }
}

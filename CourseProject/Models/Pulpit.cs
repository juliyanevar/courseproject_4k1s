using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class Pulpit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<User> Users { get; set; }
    }
}

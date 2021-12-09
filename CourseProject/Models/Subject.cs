using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PulpitId { get; set; }
        public Pulpit Pulpit { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}

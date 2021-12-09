using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class Auditorium
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AuditoriumTypeId { get; set; }
        public AuditoriumType AuditoriumType { get; set; }
        public int Capacity { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}

using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.PulpitDto
{
    public class UpdatePulpitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FacultyName { get; set; }
    }
}

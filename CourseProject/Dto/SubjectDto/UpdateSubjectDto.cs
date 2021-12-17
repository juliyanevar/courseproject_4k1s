using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.SubjectDto
{
    public class UpdateSubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PulpitName { get; set; }
    }
}

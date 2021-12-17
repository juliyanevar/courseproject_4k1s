using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.AuditoriumDto
{
    public class UpdateAuditoriumDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AuditoriumTypeName { get; set; }
        public string Capacity { get; set; }
    }
}

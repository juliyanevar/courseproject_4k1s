using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.Profession
{
    public class UpdateProfessionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FacultyName { get; set; }
    }
}

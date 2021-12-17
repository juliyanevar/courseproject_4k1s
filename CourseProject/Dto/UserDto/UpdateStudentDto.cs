using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.UserDto
{
    public class UpdateStudentDto
    {
        public string Id { get; set; }
        public string groupNumber { get; set; }
        public string course { get; set; }
        public string professionName { get; set; }
    }
}

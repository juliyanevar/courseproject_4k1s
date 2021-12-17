using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.AttendanceDto
{
    public class CreateAttendanceDto
    {
        public string DateTime { get; set; }
        public string SubjectId { get; set; }
        public string AuditoriumName { get; set; }
        public string TeacherUsername { get; set; }
    }
}

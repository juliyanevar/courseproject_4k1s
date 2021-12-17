using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.AttendanceDto
{
    public class UpdateAttendanceDto
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string SubjectName { get; set; }
        public string AuditoriumName { get; set; }
        public string TeacherUsername { get; set; }
        public string StudentUsername { get; set; }
    }
}

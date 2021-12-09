using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Guid AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }
        public Guid UserTeacherId { get; set; }
        public User UserTeacher { get; set; }
        public Guid UserStudentId { get; set; }
        public User UserStudent { get; set; }
    }
}

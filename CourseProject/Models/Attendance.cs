using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string UserTeacherId { get; set; }
        [ForeignKey("UserTeacherId")]
        [InverseProperty("TeacherAttendance")]
        public virtual User UserTeacher { get; set; }
        public string UserStudentId { get; set; }
        [ForeignKey("UserStudentId")]
        [InverseProperty("StudentAttendance")]
        public User UserStudent { get; set; }
    }
}

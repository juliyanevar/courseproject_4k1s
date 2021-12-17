using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? PulpitId { get; set; }
        public virtual Pulpit Pulpit { get; set; }
        public Guid? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Attendance> TeacherAttendance { get; set; }
        public virtual ICollection<Attendance> StudentAttendance { get; set; }
    }
}

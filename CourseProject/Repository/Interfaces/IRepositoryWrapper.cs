using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IAttendanceRepository Attendance { get; }
        IAuditoriumRepository Auditorium { get; }
        IAuditoriumTypeRepository AuditoriumType { get; }
        IFacultyRepository Faculty { get; }
        IGroupRepository Group { get; }
        IProfessionRepository Profession { get; }
        IPulpitRepository Pulpit { get; }
        ISubjectRepository Subject { get; }

        void Save();
    }
}

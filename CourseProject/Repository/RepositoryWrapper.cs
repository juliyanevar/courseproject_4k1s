using CourseProject.Models;
using CourseProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Repository
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private ApplicationContext _applicationContext;
        private IAttendanceRepository _attendance;
        private IAuditoriumRepository _auditorium;
        private IAuditoriumTypeRepository _auditoriumType;
        private IFacultyRepository _faculty;
        private IGroupRepository _group;
        private IProfessionRepository _profession;
        private IPulpitRepository _pulpit;
        private ISubjectRepository _subject;

        public IAttendanceRepository Attendance
        {
            get
            {
                if(_attendance == null)
                {
                    _attendance = new AttendanceRepository(_applicationContext);
                }
                return _attendance;
            }
        }

        public IAuditoriumRepository Auditorium
        {
            get
            {
                if (_auditorium == null)
                {
                    _auditorium = new AuditoriumRepository(_applicationContext);
                }
                return _auditorium;
            }
        }

        public IAuditoriumTypeRepository AuditoriumType
        {
            get
            {
                if (_auditoriumType == null)
                {
                    _auditoriumType = new AuditoriumTypeRepository(_applicationContext);
                }
                return _auditoriumType;
            }
        }

        public IFacultyRepository Faculty
        {
            get
            {
                if (_faculty == null)
                {
                    _faculty = new FacultyRepository(_applicationContext);
                }
                return _faculty;
            }
        }

        public IGroupRepository Group
        {
            get
            {
                if (_group == null)
                {
                    _group = new GroupRepository(_applicationContext);
                }
                return _group;
            }
        }

        public IProfessionRepository Profession
        {
            get
            {
                if (_profession == null)
                {
                    _profession = new ProfessionRepository(_applicationContext);
                }
                return _profession;
            }
        }

        public IPulpitRepository Pulpit
        {
            get
            {
                if (_pulpit == null)
                {
                    _pulpit = new PulpitRepository(_applicationContext);
                }
                return _pulpit;
            }
        }

        public ISubjectRepository Subject
        {
            get
            {
                if (_subject == null)
                {
                    _subject = new SubjectRepository(_applicationContext);
                }
                return _subject;
            }
        }

        public RepositoryWrapper(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }


        public void Save()
        {
            _applicationContext.SaveChanges();
        }
    }
}

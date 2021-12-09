using CourseProject.Models;
using CourseProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Repository
{
    public class PulpitRepository: RepositoryBase<Pulpit>, IPulpitRepository
    {
        public PulpitRepository(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }
    }
}

using CourseProject.Models;
using CourseProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Repository
{
    public class ProfessionRepository: RepositoryBase<Profession>, IProfessionRepository
    {
        public ProfessionRepository(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }
    }
}

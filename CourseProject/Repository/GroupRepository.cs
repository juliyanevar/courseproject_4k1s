using CourseProject.Models;
using CourseProject.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Repository
{
    public class GroupRepository: RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }
    }
}

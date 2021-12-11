using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.GroupDto
{
    public class UpdateGroupDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string ProfessionName { get; set; }
        public string Course { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.RoleDto
{
    public class ChangeRoleDto
    {
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Dto.UserDto
{
    public class UpdateUserInfoDto
    {
       public string userName { get; set; }
       public string email { get; set; }
       public string firstName { get; set; }
       public string lastName { get; set; }
    }
}

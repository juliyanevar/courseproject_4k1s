using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid ProfessionId { get; set; }
        public Profession Profession { get; set; }
        public int Course { get; set; }

        public ICollection<User> Users { get; set; }
    }
}

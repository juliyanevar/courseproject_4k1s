using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Auditorium> Auditorium { get; set; }
        public DbSet<AuditoriumType> AuditoriumType { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Profession> Profession { get; set; }
        public DbSet<Pulpit> Pulpit { get; set; }
        public DbSet<Subject> Subject { get; set; }
    }
}

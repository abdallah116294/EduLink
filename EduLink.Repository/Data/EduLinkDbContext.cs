using EduLink.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Repository.Data
{
    public class EduLinkDbContext:IdentityDbContext<User>
    {
        public EduLinkDbContext(DbContextOptions options):base(options)
        {
            
        }
        //Mani Entities as DbSet 
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<AcademicStaff> AcademicStaff { get; set; }
        public DbSet<NonAcademicStaff> NonAcademicStaff { get; set; }
        // Other related Entities 
        public DbSet<Class>  Classes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AcadmicYear> AcadmicYears { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            builder.ApplyConfigurationsFromAssembly(typeof(EduLinkDbContext).Assembly);
        }
    }
   
}

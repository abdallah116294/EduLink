using EduLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Repository.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.ToTable("Students");
            entity.HasKey(s => s.Id);
            entity.Property(s=>s.AdmissionNumber)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(s => s.DateOfBirth).IsRequired();
            entity.Property(s => s.EnrollmentDate).IsRequired();
            //one-to-one relationship
            entity.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict); ;
            //Many-to-One relationship with parent
            entity.HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ParentId)
                .IsRequired();
            //Many-to-one relationship with class
            entity.HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .IsRequired();
            //One-to-Many relationShips 
            entity.HasMany(s => s.Attendance)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId);
            entity.HasMany(s => s.Grade).WithOne(g => g.Student).HasForeignKey(g => g.StudentId);
            entity.HasMany(s => s.Fee).WithOne(f => f.Student).HasForeignKey(f => f.StudentId);
        }
    }
}

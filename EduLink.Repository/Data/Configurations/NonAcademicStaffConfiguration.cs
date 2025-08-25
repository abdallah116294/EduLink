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
    public class NonAcademicStaffConfiguration : IEntityTypeConfiguration<NonAcademicStaff>
    {
        public void Configure(EntityTypeBuilder<NonAcademicStaff> entity)
        {
            entity.ToTable("NonAcademicStaff");
            entity.HasKey(n=>n.Id);
            entity.Property(n => n.JobTitle)
            .IsRequired()
            .HasMaxLength(150);
            entity.HasOne(n => n.Department)
            .WithMany(d => d.NonAcademicStaff) // Assuming Department has an ICollection<NonAcademicStaff>
            .HasForeignKey(n => n.DepartmentId)
            .IsRequired();
            entity.HasOne(n => n.User)
            .WithOne() // Or WithOne(u => u.NonAcademicStaff) if User has a navigation property
            .HasForeignKey<NonAcademicStaff>(n => n.UserId)
            .IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using EduLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Repository.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.ToTable("Departments");
            entity.HasKey(d => d.Id);
            entity.Property(d => d.DepartmentName)
            .IsRequired()
            .HasMaxLength(150);
            entity.Property(d => d.HeadOfDepartmentId).IsRequired();
            entity.HasMany(d => d.AcademicStaff)
            .WithOne(a => a.Department)
            .HasForeignKey(a => a.DepartmentId)
            .IsRequired();
            entity.HasMany(d => d.NonAcademicStaff)
            .WithOne(n => n.Department)
            .HasForeignKey(n => n.DepartmentId)
            .IsRequired();
        }
    }
}

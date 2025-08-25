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
    public class AcademicStaffConfiguration : IEntityTypeConfiguration<AcademicStaff>
    {
        public void Configure(EntityTypeBuilder<AcademicStaff> entity)
        {
            entity.ToTable("AcademicStaff");
            entity.HasKey(a=>a.Id);
            entity.Property(a => a.Specialization)
            .IsRequired() // Specialization should be required
            .HasMaxLength(150);
            entity.HasOne(a => a.Department)
            .WithMany(d => d.AcademicStaff) // Assuming Department has an ICollection<AcademicStaff>
            .HasForeignKey(a => a.DepartmentId) // DepartmentId is the FK in AcademicStaff table
            .IsRequired();
            entity.HasMany(a => a.Subject)
                .WithOne(d => d.AcademicStaff)
                .HasForeignKey(s => s.AcadmicStaffId);
            entity.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<AcademicStaff>(a => a.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

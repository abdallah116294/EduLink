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
    internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SubjectName)
            .IsRequired()
            .HasMaxLength(150);
            builder.Property(s => s.Code)
           .IsRequired();
            builder.HasOne(s => s.Class)
          .WithMany(c => c.Subject) // Assuming Class has an ICollection<Subject>
          .HasForeignKey(s => s.ClassId)
          .IsRequired();
            builder.HasOne(s => s.AcademicStaff)
            .WithMany(a => a.Subject) // Assuming AcademicStaff has an ICollection<Subject>
            .HasForeignKey(s => s.AcadmicStaffId)
            .IsRequired(false);
        }
    }
}

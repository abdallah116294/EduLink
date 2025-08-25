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
    internal class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> entity)
        {
            entity.ToTable("Classes");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.ClassName)
            .IsRequired()
            .HasMaxLength(100);
            entity.Property(c => c.GradeLevel)
            .IsRequired()
            .HasMaxLength(50);
            entity.HasOne(c => c.AcadmicYear)
            .WithMany(ay => ay.Class) // Assuming AcademicYear has an ICollection<Class>
            .HasForeignKey(c => c.AcademicYearId)
            .IsRequired();
            entity.HasMany(c => c.Subject)
            .WithOne(s => s.Class)
            .HasForeignKey(s => s.ClassId) // Assuming Subject has a ClassId FK
            .IsRequired();
            entity.HasMany(c => c.Exam)
            .WithOne(e => e.Class)
            .HasForeignKey(e => e.ClassId) // Assuming Exam has a ClassId FK
            .IsRequired();
        }
    }
}

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
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");
            // Configure the primary key, which is inherited from BaseEntity
            builder.HasKey(g => g.Id);

            // Configure properties
            builder.Property(g => g.Score)
                .HasColumnType("decimal(5,2)") // Example: Scores up to 999.99
                .IsRequired(); // Score is required

            builder.Property(g => g.ExamType)
                .IsRequired() // Type of exam (e.g., "Midterm", "Final", "Quiz") is required
                .HasMaxLength(100); // Example max length for exam type

            builder.Property(g => g.Date)
                .IsRequired(); // Date the grade was recorded is required

            // Configure the many-to-one relationship with Student
            // A Grade record belongs to one Student, and a Student can have many Grade records.
            builder.HasOne(g => g.Student)
                .WithMany(s => s.Grade) // Assuming Student has an ICollection<Grade>
                .HasForeignKey(g => g.StudentId) // StudentId is the FK in the Grade table
                .IsRequired(); // A grade record must be linked to a student
        }
    }
}

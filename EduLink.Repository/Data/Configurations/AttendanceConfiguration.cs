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
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendances");
            // Configure the primary key, which is inherited from BaseEntity
            builder.HasKey(a => a.Id);

            // Configure properties
            builder.Property(a => a.Date)
                .IsRequired(); // Date of attendance is required

            builder.Property(a => a.Status)
                .IsRequired() // Attendance status (e.g., "Present", "Absent", "Late") is required
                .HasMaxLength(50); // Example max length for status

            // Configure the many-to-one relationship with Student
            // An Attendance record belongs to one Student, and a Student can have many Attendance records.
            builder.HasOne(a => a.Student)
                .WithMany(s => s.Attendance) // Assuming Student has an ICollection<Attendance>
                .HasForeignKey(a => a.StudentId) // StudentId is the FK in the Attendance table
                .IsRequired(); // An attendance record must be linked to a student
        }
    }

}

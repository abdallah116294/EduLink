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
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exams");
            // Configure the primary key, which is inherited from BaseEntity
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Name)
                .IsRequired() // Exam name is required
                .HasMaxLength(200); // Example max length for exam name

            builder.Property(e => e.Date)
                .IsRequired(); // Date of the exam is required

            // Configure the many-to-one relationship with Class
            // An Exam belongs to one Class, and a Class can have many Exams.
            builder.HasOne(e => e.Class)
                .WithMany(c => c.Exam) // Assuming Class has an ICollection<Exam>
                .HasForeignKey(e => e.ClassId) // ClassId is the FK in the Exam table
                .IsRequired(); // An exam must be linked to a class
        }
    }
}

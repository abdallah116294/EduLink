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
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.ToTable("Fees");
            // Configure the primary key, which is inherited from BaseEntity
            builder.HasKey(f => f.Id);

            // Configure properties
            builder.Property(f => f.Amount)
                .HasColumnType("decimal(18,2)") // Define precision and scale for currency
                .IsRequired(); // Amount of fee is required

            builder.Property(f => f.DueDate)
                .IsRequired(); // Due date for the fee is required

            builder.Property(f => f.Status)
                .IsRequired() // Status of the fee (e.g., "Paid", "Pending", "Overdue") is required
                .HasMaxLength(50); // Example max length for status

            // Configure the many-to-one relationship with Student
            // A Fee record belongs to one Student, and a Student can have many Fee records.
            builder.HasOne(f => f.Student)
                .WithMany(s => s.Fee) // Assuming Student has an ICollection<Fee>
                .HasForeignKey(f => f.StudentId) // StudentId is the FK in the Fee table
                .IsRequired(); // A fee record must be linked to a student
        }
    }
}

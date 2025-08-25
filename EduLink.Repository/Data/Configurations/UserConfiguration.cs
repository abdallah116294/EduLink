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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Role).HasConversion<int>();
            //Configure one-to-one relationships
            entity.HasOne(u => u.Student)
                    .WithOne(s => s.User)
                    .HasForeignKey<Student>(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(u => u.AcademicStaff)
                     .WithOne(a => a.User)
                     .HasForeignKey<AcademicStaff>(a => a.UserId)
                     .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(u => u.NonAcademicStaff)
                  .WithOne(n => n.User)
                  .HasForeignKey<NonAcademicStaff>(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(u => u.Parent)
                  .WithOne(p => p.User)
                  .HasForeignKey<Parent>(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

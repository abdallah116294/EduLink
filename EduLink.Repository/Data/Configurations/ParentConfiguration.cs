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
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> entity)
        {
            entity.ToTable("Parents");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Occupation)
            .HasMaxLength(100);
            entity.Property(p => p.Address)
            .HasMaxLength(250);
            entity.HasOne(p => p.User)
            .WithOne() 
            .HasForeignKey<Parent>(p => p.UserId)
            .IsRequired().OnDelete(DeleteBehavior.Restrict); 
            entity.HasMany(p => p.Students)
            .WithOne(s => s.Parent) 
            .HasForeignKey(s => s.ParentId) 
            .IsRequired();

        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SophosUniversityBackend.Models;

namespace SophosUniversityBackend.DBContext
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Proffesor> Proffesors { get; set; } = null!;
        public virtual DbSet<ProffesorsCourse> ProffesorsCourses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentsCourse> StudentsCourses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasOne(d => d.Proffesor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.ProffesorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Courses__proffes__2E1BDC42");
            });

            modelBuilder.Entity<ProffesorsCourse>(entity =>
            {
                entity.HasKey(e => new { e.ProffesorId, e.CourseId })
                    .HasName("PK__Proffeso__5EF6B9902006F1B2");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.ProffesorsCourse)
                    .HasForeignKey<ProffesorsCourse>(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proffesor__cours__37A5467C");

                entity.HasOne(d => d.Proffesor)
                    .WithMany(p => p.ProffesorsCourses)
                    .HasForeignKey(d => d.ProffesorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proffesor__proff__36B12243");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Students__depart__286302EC");
            });

            modelBuilder.Entity<StudentsCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId })
                    .HasName("PK__Students__D2C2E9E059A52BCE");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentsCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentsC__cours__32E0915F");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentsCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentsC__stude__31EC6D26");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

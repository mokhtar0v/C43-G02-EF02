using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session2_EF.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=UniversityDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.HiringDate).HasColumnType("date");
                
                // Relationship with Instructor (Department head)
                entity.HasOne(d => d.HeadInstructor)
                      .WithMany(i => i.DepartmentsHeaded)
                      .HasForeignKey(d => d.Ins_ID)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<CourseInstructor>(entity =>
            {
                entity.ToTable("Course_Inst");
                entity.HasKey(e => new { e.Inst_ID, e.Course_ID });
                entity.Property(e => e.Evaluate).HasMaxLength(50);
                
                // Relationships
                entity.HasOne(ci => ci.Instructor)
                      .WithMany(i => i.CourseInstructors)
                      .HasForeignKey(ci => ci.Inst_ID)
                      .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(ci => ci.Course)
                      .WithMany(c => c.CourseInstructors)
                      .HasForeignKey(ci => ci.Course_ID)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentID, sc.CourseID });
                
                // Relationships
                entity.HasOne(sc => sc.Student)
                      .WithMany(s => s.StudentCourses)
                      .HasForeignKey(sc => sc.StudentID)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(sc => sc.Course)
                      .WithMany(c => c.StudentCourses)
                      .HasForeignKey(sc => sc.CourseID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //Student
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.Dep_Id)
                .OnDelete(DeleteBehavior.Restrict);

            //Course
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Topic)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.Top_ID)
                .OnDelete(DeleteBehavior.Restrict);

            //Instructor
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.Dept_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

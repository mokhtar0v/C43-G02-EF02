using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session2_EF.Data
{
    public class Student
    {
        public int ID { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        [ForeignKey("Dep_Id")]
        public int Dep_Id { get; set; }
        public Department Department { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
    [Table("Course")]
    public class Course
    {
        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
            CourseInstructors = new HashSet<CourseInstructor>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("Description")]
        public string? CourseDescription { get; set; }

        [ForeignKey("Top_ID")]
        public int Top_ID { get; set; }
        public Topic Topic { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<CourseInstructor> CourseInstructors { get; set; }
    }
    public class Department //Fluent API
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Instructors = new HashSet<Instructor>();
        }

        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime HiringDate { get; set; }
        [ForeignKey("Ins_ID")] //self relation
        public int Ins_ID { get; set; }
        public Instructor HeadInstructor { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
    [Table("Instructor")]
    public class Instructor
    {
        public Instructor()
        {
            DepartmentsHeaded = new HashSet<Department>();
            InstructorDepartment = new HashSet<Department>();
            CourseInstructors = new HashSet<CourseInstructor>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }

        public int Hours { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public string? Adress { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal HourRate { get; set; }

        [ForeignKey("Dept_ID")]
        public int Dept_ID { get; set; }
        public Department Department { get; set; }

        public ICollection<Department> DepartmentsHeaded { get; set; }

        public ICollection<Department> InstructorDepartment { get; set; }

        public ICollection<CourseInstructor> CourseInstructors { get; set; }
    }
    public class Topic
    {
        public Topic()
        {
            Courses = new HashSet<Course>();
        }
        public int ID { get; set; }
        public string? Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
    [Table("Stud_Course")]
    public class StudentCourse
    {
        [Column("stud_ID")]
        public int StudentID { get; set; }

        [Column("Course_ID")]
        public int CourseID { get; set; }

        [Column("Grade")]
        public string? Grade { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }
    }
    public class CourseInstructor //Fluent API
    {
        public int Inst_ID { get; set; }
        public int Course_ID { get; set; }
        public string? Evaluate { get; set; }
        [ForeignKey("Inst_ID")]
        public Instructor Instructor { get; set; }

        [ForeignKey("Course_ID")]
        public Course Course { get; set; }

    }
}

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
        public int Dep_Id { get; set; }
    }
    [Table("Course")]
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("Description")]
        public string? CourseDescription { get; set; }

        public int Top_ID { get; set; }
    }
    public class Department //Fluent API
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Ins_ID { get; set; }
        public DateTime HiringDate { get; set; }
    }
    [Table("Instructor")]
    public class Instructor
    {
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

        public int Dept_ID { get; set; }
    }
    public class Topic
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }
    [Table("Stud_Course")]
    public class StudentCourse
    {
        [Column("stud_ID")]
        public int StudentID { get; set; }

        [Column("Course_ID")]
        public int CourseID { get; set; }

        public string? Grade { get; set; }
    }
    public class CourseInstructor //Fluent API
    {
        public int Inst_ID { get; set; }
        public int Course_ID { get; set; }
        public string? Evaluate { get; set; }
    }
}

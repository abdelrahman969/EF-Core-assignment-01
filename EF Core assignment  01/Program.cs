using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// =============================================
// CONVENTION WAY (No Attributes) - ALL TABLES
// =============================================

public class Student
{
    public int ID { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Address { get; set; }
    public int? Age { get; set; }
    public int? Dep_Id { get; set; }
}

public class Course
{
    public int ID { get; set; }
    public string Duration { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Top_ID { get; set; }
}

public class Topic
{
    public int ID { get; set; }
    public string Name { get; set; }
}

public class CourseInst
{
    public int inst_ID { get; set; }
    public int Course_ID { get; set; }
    public string evaluate { get; set; }
}

public class StudCourse
{
    public int stud_ID { get; set; }
    public int Course_ID { get; set; }
    public string Grade { get; set; }
}

public class Department
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int? Ins_ID { get; set; }
    public DateTime? HiringDate { get; set; }
}

public class Instructor
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public string Address { get; set; }
    public decimal? HourRate { get; set; }
    public decimal? Bouns { get; set; }
    public int? Dept_ID { get; set; }
}

// =============================================
// DATA ANNOTATIONS WAY (Single Key Tables ONLY)
// =============================================

[Table("StudentsDA")]
public class StudentDA
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(50)]
    public string FName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LName { get; set; }

    [MaxLength(100)]
    public string Address { get; set; }

    public int? Age { get; set; }

    public int? Dep_Id { get; set; }
}

[Table("CoursesDA")]
public class CourseDA
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public string Duration { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public int? Top_ID { get; set; }
}

[Table("TopicsDA")]
public class TopicDA
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}

[Table("DepartmentsDA")]
public class DepartmentDA
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public int? Ins_ID { get; set; }

    public DateTime? HiringDate { get; set; }
}

[Table("InstructorsDA")]
public class InstructorDA
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public decimal Salary { get; set; }

    [MaxLength(100)]
    public string Address { get; set; }

    public decimal? HourRate { get; set; }

    public decimal? Bouns { get; set; }

    public int? Dept_ID { get; set; }
}

// =============================================
// DBCONTEXT
// =============================================

public class ItiContext : DbContext
{
    // Convention Way DbSets (ALL TABLES)
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<CourseInst> CourseInsts { get; set; }
    public DbSet<StudCourse> StudCourses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }

    // Data Annotations Way DbSets (SINGLE KEY TABLES ONLY - No Composite Keys)
    public DbSet<StudentDA> StudentsDA { get; set; }
    public DbSet<CourseDA> CoursesDA { get; set; }
    public DbSet<TopicDA> TopicsDA { get; set; }
    public DbSet<DepartmentDA> DepartmentsDA { get; set; }
    public DbSet<InstructorDA> InstructorsDA { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Database=ItiDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Convention Way Configurations for Composite Keys
        modelBuilder.Entity<CourseInst>()
            .HasKey(ci => new { ci.inst_ID, ci.Course_ID });

        modelBuilder.Entity<StudCourse>()
            .HasKey(sc => new { sc.stud_ID, sc.Course_ID });
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("ITI Database - Both Mapping Ways");
    }
}
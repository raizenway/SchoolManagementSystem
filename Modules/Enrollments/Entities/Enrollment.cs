using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Modules.Classes.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Modules.Enrollments.Entities;

public class Enrollment
{
    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public int ClassId { get; set; }
    public Class? Class { get; set; }

    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}

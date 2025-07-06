using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SchoolManagementSystem.Configurations;
using SchoolManagementSystem.Modules.Enrollments.Entities;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Modules.Classes.Entities;
namespace SchoolManagementSystem.Modules.Enrollments;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public EnrollmentController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Enroll(int studentId, int classId)
    {
        // Pastikan student & class benar-benar ada
        var studentExists = _db.Students.Any(s => s.Id == studentId);
        var classExists = _db.Classes.Any(c => c.Id == classId);
    
        if (!studentExists || !classExists)
            return NotFound("Student or Class not found.");
    
        var exists = _db.Enrollments.Any(e => e.StudentId == studentId && e.ClassId == classId);
        if (exists) return Conflict("Student already enrolled in this class.");
    
        var enrollment = new Enrollment
        {
            StudentId = studentId,
            ClassId = classId
            // Tidak usah set Student atau Class di sini
        };
    
        _db.Enrollments.Add(enrollment);
        _db.SaveChanges();
    
        // Ambil ulang dengan Include biar return lengkap
        var result = _db.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Class)
                .ThenInclude(c => c.Teacher)
            .First(e => e.StudentId == studentId && e.ClassId == classId);
    
        return Ok(result);
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        var list = _db.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Class)
            .ToList();

        return Ok(list);
    }

    [HttpDelete]
    public IActionResult Unenroll(int studentId, int classId)
    {
        var enrollment = _db.Enrollments.Find(studentId, classId);
        if (enrollment == null) return NotFound();

        _db.Enrollments.Remove(enrollment);
        _db.SaveChanges();
        return NoContent();
    }
}

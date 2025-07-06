using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Configurations;
using Microsoft.AspNetCore.Authorization;
namespace SchoolManagementSystem.Modules.Students;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public StudentController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var students = _db.Students.ToList();
        return Ok(students);
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        _db.Students.Add(student);
        _db.SaveChanges();
        return Ok(student);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Student updated)
    {
        var student = _db.Students.Find(id);
        if (student == null) return NotFound();

        student.FullName = updated.FullName;
        student.Email = updated.Email;
        student.DateOfBirth = updated.DateOfBirth;
        _db.SaveChanges();

        return Ok(student);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = _db.Students.Find(id);
        if (student == null) return NotFound();

        _db.Students.Remove(student);
        _db.SaveChanges();

        return NoContent();
    }
}

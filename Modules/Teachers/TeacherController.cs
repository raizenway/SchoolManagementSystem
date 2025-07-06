using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Modules.Teachers.Entities;
using SchoolManagementSystem.Configurations;
using Microsoft.AspNetCore.Authorization;
namespace SchoolManagementSystem.Modules.Teachers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public TeacherController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var teachers = _db.Teachers.ToList();
        return Ok(teachers);
    }

    [HttpPost]
    public IActionResult Create(Teacher teacher)
    {
        _db.Teachers.Add(teacher);
        _db.SaveChanges();
        return Ok(teacher);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Teacher updated)
    {
        var teacher = _db.Teachers.Find(id);
        if (teacher == null) return NotFound();

        teacher.FullName = updated.FullName;
        teacher.Email = updated.Email;
        teacher.Subject = updated.Subject;
        _db.SaveChanges();

        return Ok(teacher);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var teacher = _db.Teachers.Find(id);
        if (teacher == null) return NotFound();

        _db.Teachers.Remove(teacher);
        _db.SaveChanges();

        return NoContent();
    }
}

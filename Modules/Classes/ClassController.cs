using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SchoolManagementSystem.Configurations;
using SchoolManagementSystem.Modules.Classes.Entities;

namespace SchoolManagementSystem.Modules.Classes;

[ApiController]
[Route("api/[controller]")]
public class ClassController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ClassController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var classes = _db.Classes.Include(c => c.Teacher).ToList();
        return Ok(classes);
    }

    [HttpPost]
    public IActionResult Create(Class c)
    {
        _db.Classes.Add(c);
        _db.SaveChanges();
        return Ok(c);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Class updated)
    {
        var cls = _db.Classes.Find(id);
        if (cls == null) return NotFound();

        cls.Name = updated.Name;
        _db.SaveChanges();

        return Ok(cls);
    }

    [HttpPut("{id}/assign-teacher/{teacherId}")]
    public IActionResult AssignTeacher(int id, int teacherId)
    {
        var cls = _db.Classes.Find(id);
        if (cls == null) return NotFound();

        var teacher = _db.Teachers.Find(teacherId);
        if (teacher == null) return NotFound("Teacher not found");

        cls.TeacherId = teacherId;
        _db.SaveChanges();

        return Ok(cls);
    }

    [HttpPut("{id}/unassign-teacher")]
    public IActionResult UnassignTeacher(int id)
    {
        var cls = _db.Classes.Find(id);
        if (cls == null) return NotFound();

        cls.TeacherId = null;
        _db.SaveChanges();

        return Ok(cls);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var cls = _db.Classes.Find(id);
        if (cls == null) return NotFound();

        _db.Classes.Remove(cls);
        _db.SaveChanges();

        return NoContent();
    }
}

namespace SchoolManagementSystem.Modules.Classes.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Class
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Optional: relasi ke Teacher
    public int? TeacherId { get; set; }
    public Teachers.Entities.Teacher? Teacher { get; set; }
}

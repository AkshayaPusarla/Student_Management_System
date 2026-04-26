using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRepository
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Roll Number is required")]
        [MaxLength(50)]
        public string RollNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Course { get; set; }
    }
}
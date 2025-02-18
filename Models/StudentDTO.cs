using System.ComponentModel.DataAnnotations;

namespace IdentityPractice.Models
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}


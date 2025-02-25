using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace IdentityPractice.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        
        [JsonIgnore]
        public List<Enrollment>? Enrollments { get; set; }
    }
}
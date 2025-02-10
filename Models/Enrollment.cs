using System.Text.Json.Serialization;

namespace IdentityPractice.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
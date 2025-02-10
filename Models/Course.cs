using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IdentityPractice.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public List<Enrollment>? Enrollments { get; set; }
    }
}

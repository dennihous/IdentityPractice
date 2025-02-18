using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityPractice.Models;

namespace IdentityPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                _logger.LogInformation("Fetching all students.");
                var students = await _studentService.GetAllStudentsAsync();
                if (students == null || !students.Any())
                {
                    _logger.LogWarning("No students found.");
                    return NotFound("No students found.");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching students.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching student with ID {id}");
                var student = await _studentService.GetStudentByIdAsync(id);

                if (student == null)
                {
                    _logger.LogWarning($"Student with ID {id} not found.");
                    return NotFound($"Student with ID {id} not found.");
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the student.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            try
            {
                if (id != student.StudentId)
                {
                    _logger.LogWarning("Student ID mismatch.");
                    return BadRequest("Student ID mismatch.");
                }

                await _studentService.UpdateStudentAsync(id, student);
                _logger.LogInformation($"Student with ID {id} updated.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _studentService.GetStudentByIdAsync(id) == null)
                {
                    _logger.LogWarning($"Student with ID {id} not found for update.");
                    return NotFound($"Student with ID {id} not found.");
                }
                else
                {
                    _logger.LogError("Error updating student.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the student.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    _logger.LogWarning("Received empty student object.");
                    return BadRequest("Student data cannot be null.");
                }

                await _studentService.AddStudentAsync(student);
                _logger.LogInformation($"Student with ID {student.StudentId} created.");
                return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the student.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    _logger.LogWarning($"Student with ID {id} not found.");
                    return NotFound($"Student with ID {id} not found.");
                }

                await _studentService.DeleteStudentAsync(id);
                _logger.LogInformation($"Student with ID {id} deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the student.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}

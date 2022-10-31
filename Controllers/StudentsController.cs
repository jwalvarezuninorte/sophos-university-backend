using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosUniversityBackend.DBContext;
using SophosUniversityBackend.DTOs;
using SophosUniversityBackend.Models;

namespace SophosUniversityBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            // return await _context.Students.Select(s => s.ToDto()).ToListAsync();

            return await _context.Students.Include(d => d.Department).Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.FirstName + " " + s.LastName,
                DepartmentId = s.DepartmentId,
                DepartmentName = s.Department.DepartmentName ?? "Sin facultad",
                TotalCredits = s.StudentsCourses.Sum(c => c.Course.Credits),
            }).OrderByDescending(s => s.Id).ToListAsync();
            // return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetailDto>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.Include(d => d.Department).Select(s => new StudentDetailDto
            {
                Id = s.Id,
                Name = s.FirstName + " " + s.LastName,
                DepartmentId = s.DepartmentId,
                DepartmentName = s.Department.DepartmentName ?? "Sin facultad",
                TotalCredits = s.StudentsCourses.Sum(c => c.Course.Credits),
                Courses = s.StudentsCourses.Select(c => new SimpleStudentCourseDto
                {
                    Id = c.CourseId,
                    Name = c.Course.Title,
                    Credits = c.Course.Credits,
                    TotalStudents = c.Course.StudentsCourses.Count,
                }).ToList()
            }).FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;

        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutStudent(UpdateStudentDto student)
        {
            var studentToUpdate = await _context.Students.FindAsync(student.Id);
            if (studentToUpdate == null)
            {
                return NotFound();
            }

            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.DepartmentId = student.DepartmentId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!StudentExists(student.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentDto>> PostStudent(CreateStudentDto student)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'AppDbContext.Students'  is null.");
            }


            var newStudent = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DepartmentId = student.DepartmentId
            };

            _context.Students.Add(newStudent);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = newStudent.Id }, new StudentDto
            {
                Id = newStudent.Id,
                Name = newStudent.FirstName + " " + newStudent.LastName,
                DepartmentId = newStudent.DepartmentId,
            });
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

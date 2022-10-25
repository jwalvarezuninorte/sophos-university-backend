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
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SimpleCourseDto>>> GetCourses()
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }

            return await _context.Courses.Select(c => new SimpleCourseDto
            {
                Id = c.Id,
                Name = c.Title,
                Credits = c.Credits,
                TotalStudents = c.StudentsCourses.Count,
                Limit = c.Limit,
            }).ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDetailDto>> GetCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.Select(c => new CourseDetailDto
            {
                Id = c.Id,
                Name = c.Title,
                Description = c.Description,
                Credits = c.Credits,
                Limit = c.Limit,
                TotalStudents = c.StudentsCourses.Count,
                ProffesorName = c.Proffesor.FirstName + " " + c.Proffesor.LastName,
                Students = c.StudentsCourses.Select(s => new StudentDto
                {
                    Id = s.Student.Id,
                    Name = s.Student.FirstName + " " + s.Student.LastName,
                    DepartmentId = s.Student.DepartmentId,
                    DepartmentName = s.Student.Department.DepartmentName ?? "Sin facultad",
                    TotalCredits = s.Student.StudentsCourses.Sum(c => c.Course.Credits),
                }).ToList(),
            }).FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCourse(UpdateCourseDto course)
        {

            var courseToUpdate = await _context.Courses.FindAsync(course.Id);
            if (courseToUpdate == null)
            {
                return NotFound();
            }

            courseToUpdate.Title = course.Name;
            courseToUpdate.Description = course.Description;
            courseToUpdate.Credits = course.Credits;
            courseToUpdate.Limit = course.Limit;
            courseToUpdate.ProffesorId = course.ProffesorId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(course.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SimpleCourseDto>> PostCourse(CreateCourseDto course)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'AppDbContext.Courses'  is null.");
            }

            var newCourse = new Course
            {
                Title = course.Name,
                Description = course.Description,
                Credits = course.Credits,
                Limit = course.Limit,
                ProffesorId = course.ProffesorId,
            };

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = newCourse.Id }, new SimpleCourseDto
            {
                Id = newCourse.Id,
                Name = newCourse.Title,
                Credits = newCourse.Credits,
                TotalStudents = newCourse.StudentsCourses.Count,
                Limit = newCourse.Limit,
            });
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosUniversityBackend.DBContext;
using SophosUniversityBackend.Models;

namespace SophosUniversityBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsCourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsCourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentsCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentsCourse>>> GetStudentsCourses()
        {
          if (_context.StudentsCourses == null)
          {
              return NotFound();
          }
            return await _context.StudentsCourses.ToListAsync();
        }

        // GET: api/StudentsCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentsCourse>> GetStudentsCourse(int id)
        {
          if (_context.StudentsCourses == null)
          {
              return NotFound();
          }
            var studentsCourse = await _context.StudentsCourses.FindAsync(id);

            if (studentsCourse == null)
            {
                return NotFound();
            }

            return studentsCourse;
        }

        // PUT: api/StudentsCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentsCourse(int id, StudentsCourse studentsCourse)
        {
            if (id != studentsCourse.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(studentsCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsCourseExists(id))
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

        // POST: api/StudentsCourse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentsCourse>> PostStudentsCourse(StudentsCourse studentsCourse)
        {
          if (_context.StudentsCourses == null)
          {
              return Problem("Entity set 'AppDbContext.StudentsCourses'  is null.");
          }
            _context.StudentsCourses.Add(studentsCourse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentsCourseExists(studentsCourse.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentsCourse", new { id = studentsCourse.StudentId }, studentsCourse);
        }

        // DELETE: api/StudentsCourse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentsCourse(int id)
        {
            if (_context.StudentsCourses == null)
            {
                return NotFound();
            }
            var studentsCourse = await _context.StudentsCourses.FindAsync(id);
            if (studentsCourse == null)
            {
                return NotFound();
            }

            _context.StudentsCourses.Remove(studentsCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentsCourseExists(int id)
        {
            return (_context.StudentsCourses?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}

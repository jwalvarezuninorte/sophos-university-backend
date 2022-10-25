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
    public class ProffesorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProffesorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Proffesors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProffesorDto>>> GetProffesors()
        {
            if (_context.Proffesors == null)
            {
                return NotFound();
            }

            return await _context.Proffesors.Select(p => new ProffesorDto
            {
                Id = p.Id,
                Name = p.FirstName + " " + p.LastName,
                MaximumDegree = p.MaximumDegree,
                YearsOfExperience = p.YearsOfExperience,
                Courses = p.ProffesorsCourses.Select(c => new SimpleProffesorCourseDto
                {
                    Id = c.CourseId,
                    Name = c.Course.Title,
                    Credits = c.Course.Credits,
                }).ToList(),
            }).ToListAsync();
        }

        // GET: api/Proffesors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProffesorDto>> GetProffesor(int id)
        {
            if (_context.Proffesors == null)
            {
                return NotFound();
            }
            var proffesor = await _context.Proffesors.Select(p => new ProffesorDto
            {
                Id = p.Id,
                Name = p.FirstName + " " + p.LastName,
                MaximumDegree = p.MaximumDegree,
                YearsOfExperience = p.YearsOfExperience,
                Courses = p.ProffesorsCourses.Select(c => new SimpleProffesorCourseDto
                {
                    Id = c.CourseId,
                    Name = c.Course.Title,
                    Credits = c.Course.Credits,
                }).ToList(),
            }).FirstOrDefaultAsync(p => p.Id == id);

            if (proffesor == null)
            {
                return NotFound();
            }

            return proffesor;
        }

        // PUT: api/Proffesors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProffesor(UpdateProffesorDto proffesor)
        {
            var proffesorToUpdate = await _context.Proffesors.FindAsync(proffesor.Id);
            if (proffesorToUpdate == null)
            {
                return NotFound();
            }

            proffesorToUpdate.FirstName = proffesor.FirstName;
            proffesorToUpdate.LastName = proffesor.LastName;
            proffesorToUpdate.MaximumDegree = proffesor.MaximumDegree;
            proffesorToUpdate.YearsOfExperience = proffesor.YearsOfExperience;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProffesorExists(proffesor.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Proffesors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProffesorDto>> PostProffesor(CreateProffesorDto proffesor)
        {
            if (_context.Proffesors == null)
            {
                return Problem("Entity set 'AppDbContext.Proffesors'  is null.");
            }

            var newProffesor = new Proffesor
            {
                FirstName = proffesor.FirstName,
                LastName = proffesor.LastName,
                MaximumDegree = proffesor.MaximumDegree,
                YearsOfExperience = proffesor.YearsOfExperience,
            };



            _context.Proffesors.Add(newProffesor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProffesor", new { id = newProffesor.Id }, new ProffesorDto
            {
                Id = newProffesor.Id,
                Name = newProffesor.FirstName + " " + newProffesor.LastName,
                MaximumDegree = newProffesor.MaximumDegree,
                YearsOfExperience = newProffesor.YearsOfExperience,
            });
        }

        // DELETE: api/Proffesors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProffesor(int id)
        {
            if (_context.Proffesors == null)
            {
                return NotFound();
            }
            var proffesor = await _context.Proffesors.FindAsync(id);
            if (proffesor == null)
            {
                return NotFound();
            }

            _context.Proffesors.Remove(proffesor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProffesorExists(int id)
        {
            return (_context.Proffesors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

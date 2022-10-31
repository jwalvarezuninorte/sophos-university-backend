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
    public class ProffesorsCourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProffesorsCourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProffesorsCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProffesorsCourse>>> GetProffesorsCourses()
        {
          if (_context.ProffesorsCourses == null)
          {
              return NotFound();
          }
            return await _context.ProffesorsCourses.ToListAsync();
        }

        // GET: api/ProffesorsCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProffesorsCourse>> GetProffesorsCourse(int id)
        {
          if (_context.ProffesorsCourses == null)
          {
              return NotFound();
          }
            var proffesorsCourse = await _context.ProffesorsCourses.FindAsync(id);

            if (proffesorsCourse == null)
            {
                return NotFound();
            }

            return proffesorsCourse;
        }

        // PUT: api/ProffesorsCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProffesorsCourse(int id, ProffesorsCourse proffesorsCourse)
        {
            if (id != proffesorsCourse.ProffesorId)
            {
                return BadRequest();
            }

            _context.Entry(proffesorsCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProffesorsCourseExists(id))
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

        // POST: api/ProffesorsCourse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProffesorsCourse>> PostProffesorsCourse(ProffesorsCourse proffesorsCourse)
        {
          if (_context.ProffesorsCourses == null)
          {
              return Problem("Entity set 'AppDbContext.ProffesorsCourses'  is null.");
          }
            _context.ProffesorsCourses.Add(proffesorsCourse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProffesorsCourseExists(proffesorsCourse.ProffesorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProffesorsCourse", new { id = proffesorsCourse.ProffesorId }, proffesorsCourse);
        }

        // DELETE: api/ProffesorsCourse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProffesorsCourse(int id)
        {
            if (_context.ProffesorsCourses == null)
            {
                return NotFound();
            }
            var proffesorsCourse = await _context.ProffesorsCourses.FindAsync(id);
            if (proffesorsCourse == null)
            {
                return NotFound();
            }

            _context.ProffesorsCourses.Remove(proffesorsCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProffesorsCourseExists(int id)
        {
            return (_context.ProffesorsCourses?.Any(e => e.ProffesorId == id)).GetValueOrDefault();
        }
    }
}

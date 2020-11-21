using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Condolencia.Data;
using Condolencia.Models;

namespace Condolencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemModeradasController : ControllerBase
    {
        private readonly CondolenciaContext _context;

        public MensagemModeradasController(CondolenciaContext context)
        {
            _context = context;
        }

        // GET: api/MensagemModeradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MensagemModerada>>> GetMensagemModerada()
        {
            return await _context.MensagemModerada.ToListAsync();
        }

        // GET: api/MensagemModeradas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MensagemModerada>> GetMensagemModerada(int id)
        {
            var mensagemModerada = await _context.MensagemModerada.FindAsync(id);

            if (mensagemModerada == null)
            {
                return NotFound();
            }

            return mensagemModerada;
        }

        // PUT: api/MensagemModeradas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensagemModerada(int id, MensagemModerada mensagemModerada)
        {
            if (id != mensagemModerada.Id)
            {
                return BadRequest();
            }

            _context.Entry(mensagemModerada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensagemModeradaExists(id))
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

        // POST: api/MensagemModeradas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MensagemModerada>> PostMensagemModerada(MensagemModerada mensagemModerada)
        {
            _context.MensagemModerada.Add(mensagemModerada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMensagemModerada", new { id = mensagemModerada.Id }, mensagemModerada);
        }

        // DELETE: api/MensagemModeradas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensagemModerada(int id)
        {
            var mensagemModerada = await _context.MensagemModerada.FindAsync(id);
            if (mensagemModerada == null)
            {
                return NotFound();
            }

            _context.MensagemModerada.Remove(mensagemModerada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MensagemModeradaExists(int id)
        {
            return _context.MensagemModerada.Any(e => e.Id == id);
        }
    }
}

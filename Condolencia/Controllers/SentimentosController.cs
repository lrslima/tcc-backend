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
    public class SentimentosController : ControllerBase
    {
        private readonly CondolenciaContext _context;

        public SentimentosController(CondolenciaContext context)
        {
            _context = context;
        }

        // GET: api/Sentimentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sentimento>>> GetSentimento()
        {
            return await _context.Sentimento.ToListAsync();
        }

        // GET: api/Sentimentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sentimento>> GetSentimento(int id)
        {
            var sentimento = await _context.Sentimento.FindAsync(id);

            if (sentimento == null)
            {
                return NotFound();
            }

            return sentimento;
        }

        // PUT: api/Sentimentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSentimento(int id, Sentimento sentimento)
        {
            if (id != sentimento.Id)
            {
                return BadRequest();
            }

            _context.Entry(sentimento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SentimentoExists(id))
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

        // POST: api/Sentimentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sentimento>> PostSentimento(Sentimento sentimento)
        {
            _context.Sentimento.Add(sentimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSentimento", new { id = sentimento.Id }, sentimento);
        }

        // DELETE: api/Sentimentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSentimento(int id)
        {
            var sentimento = await _context.Sentimento.FindAsync(id);
            if (sentimento == null)
            {
                return NotFound();
            }

            _context.Sentimento.Remove(sentimento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SentimentoExists(int id)
        {
            return _context.Sentimento.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Condolencia.Data;
using Condolencia.Models;
using Condolencia.Interfaces;
using Condolencia.DTOs;

namespace Condolencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitimasController : ControllerBase
    {
        private readonly CondolenciaContext _context;
        private readonly IVitimaService _vitimaContext;

        public VitimasController(CondolenciaContext context, IVitimaService vitimaService)
        {
            _context = context;
            _vitimaContext = vitimaService;
        }

        // GET: api/Vitimas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vitima>>> GetVitima()
        {
            return await _context.Vitima.ToListAsync();
        }

        // GET: api/Vitimas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vitima>> GetVitima(int id)
        {
            var vitima = await _context.Vitima.FindAsync(id);

            if (vitima == null)
            {
                return NotFound(new { Mensagem = "Vítima inexistente ou inválida." });
            }

            return vitima;
        }

        // PUT: api/Vitimas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVitima(int id, Vitima vitima)
        {
            if (id != vitima.Id)
            {
                return BadRequest();
            }

            _context.Entry(vitima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Vitimas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VitimaViewModel>> PostVitima(VitimaViewModel vitimaViewModel)
        {
            Vitima vitima = new Vitima();

            try
            {
                // Fazer validações
                await _vitimaContext.CadastrarVitima(vitimaViewModel);

                vitima.Nome = vitimaViewModel.nome;
                vitima.SobreNome = vitimaViewModel.sobrenome;
                vitima.CPF = vitimaViewModel.cpf.Trim();
                vitima.RG = vitimaViewModel.rg.Trim();
                vitima.Rua = vitimaViewModel.endereco_rua;
                vitima.Cidade = vitimaViewModel.endereco_cidade;
                vitima.Estado = vitimaViewModel.endereco_estado;
                vitima.Fotografia = vitimaViewModel.imagem;
                _context.Vitima.Add(vitima);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetVitima", new { id = vitima.Id }, vitima);
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        // DELETE: api/Vitimas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVitima(int id)
        {
            var vitima = await _context.Vitima.FindAsync(id);
            if (vitima == null)
            {
                return NotFound();
            }

            _context.Vitima.Remove(vitima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VitimaExists(int id)
        {
            return _context.Vitima.Any(e => e.Id == id);
        }
    }
}

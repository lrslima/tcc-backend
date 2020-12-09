using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Condolencia.Data;
using Condolencia.Models;
using Condolencia.DTOs;
using Condolencia.Services;
using Condolencia.Interfaces;

namespace Condolencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepoimentosController : ControllerBase
    {
        private readonly CondolenciaContext _context;
        private readonly IDepoimentoService _depoimentoService;
        public DepoimentosController(CondolenciaContext context, IDepoimentoService depoimentoService)
        {
            _context = context;
            _depoimentoService = depoimentoService;
        }

        // GET: api/Depoimentos por status/5
        [HttpGet("status")]
        public async Task<ActionResult<List<DepoimentoRegistrar>>> GetDepoimentoByStatus(string status)
        {
            var result = await _depoimentoService.GetDepoimentoByStatus(status);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet("QRcode")]
        public async Task<ActionResult<List<DepoimentoRegistrar>>> GetQrCode()
        {
            var result = await _depoimentoService.GetQrCode();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet("id")]
        [Produces("application/json")]
        public async Task<ActionResult<DepoimentoRegistrar>> GetDepoimento(int id)
        {
            var depoimento = await _depoimentoService.GetDepoimento(id);

            if (depoimento == null)
            {
                return NotFound();
            }

            return depoimento;
        }

        [HttpPut("id")]
        [Produces("application/json")]
        public async Task<ActionResult<DepoimentoRegistrar>> PutDepoimento(int id, [FromBody] DepoimentoModeradoViewModel depoimentoModerado)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }

            depoimentoModerado.IdDepoimento = id;
            var result = await _depoimentoService.AlterarStatus(depoimentoModerado);

            return result;
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<DepoimentoRegistrar>> PostDepoimento([FromBody] DepoimentoRegistrar publicarDepoimento)
        {
            var result = await _depoimentoService.RegistrarDepoimento(publicarDepoimento);

            return result;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<DepoimentoRegistrar>>> GetMensagens()
        {
            var result = await _depoimentoService.GetAllDepoimentos();

            return result;
        }

        // DELETE: api/Depoimentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepoimento(int id)
        {
            var depoimento = await _context.Depoimento.FindAsync(id);
            if (depoimento == null)
            {
                return NotFound();
            }

            _context.Depoimento.Remove(depoimento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepoimentoExists(int id)
        {
            return _context.Depoimento.Any(e => e.Id == id);
        }
    }
}

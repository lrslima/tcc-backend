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
            try
            {
                var result = await _depoimentoService.GetDepoimentoByStatus(status);

                if (result == null)
                {
                    return NotFound(new { Mensagem = "Nenhum depoimento encontrado." });
                }

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("QRcode")]
        public async Task<ActionResult<List<DepoimentoRegistrar>>> GetQrCode()
        {
            try
            {
                var result = await _depoimentoService.GetQrCode();

                if (result == null)
                {
                    return NotFound(new { Mensagem = "Nenhum depoimento encontrado" });
                }

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("id")]
        [Produces("application/json")]
        public async Task<ActionResult<DepoimentoRegistrar>> GetDepoimento(int id)
        {
            try
            {
                var depoimento = await _depoimentoService.GetDepoimento(id);

                if (depoimento == null)
                {
                    return NotFound(new { Mensagem = "Nenhum depoimento encontrado." });
                }

                return depoimento;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpPut("id")]
        [Produces("application/json")]
        public async Task<ActionResult<DepoimentoRegistrar>> PutDepoimento(int id, [FromBody] DepoimentoModeradoViewModel depoimentoModerado)
        {
            try
            {
                if (id == 0 || id < 0)
                {
                    return BadRequest(new { Mensagem = "Id informado não é válido." });
                }

                depoimentoModerado.IdDepoimento = id;
                var result = await _depoimentoService.AlterarStatus(depoimentoModerado);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<DepoimentoRegistrar>> PostDepoimento([FromBody] DepoimentoRegistrar publicarDepoimento)
        {
            try
            {
                var result = await _depoimentoService.RegistrarDepoimento(publicarDepoimento);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<DepoimentoRegistrar>>> GetMensagens()
        {
            try
            {
                var result = await _depoimentoService.GetAllDepoimentos();

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        // DELETE: api/Depoimentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepoimento(int id)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        private bool DepoimentoExists(int id)
        {
            return _context.Depoimento.Any(e => e.Id == id);
        }
    }
}

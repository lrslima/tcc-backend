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
    public class PessoasController : ControllerBase
    {
        private readonly CondolenciaContext _context;
        private readonly IPessoaService _pessoaService;

        public PessoasController(CondolenciaContext context, IPessoaService pessoaService)
        {
            _context = context;
            _pessoaService = pessoaService;
        }

        // GET: api/Pessoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoa()
        {
            return await _context.Pessoa.ToListAsync();
        }

        // GET: api/Pessoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound(new { Mensagem = "Pessoa inexistente ou inválida." });
            }

            return pessoa;
        }

        // PUT: api/Pessoas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Pessoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PessoaViewModel>> PostPessoa(PessoaViewModel pessoaViewModel)
        {
            Pessoa pessoa = new Pessoa();

            try
            {
                // Fazer validações
                await _pessoaService.CadastrarPessoa(pessoaViewModel);

                // Salvar inclusão de Pessoa
                pessoa.Nome = pessoaViewModel.nome;
                pessoa.SobreNome = pessoaViewModel.sobrenome;
                pessoa.CPF = pessoaViewModel.cpf.Trim();
                pessoa.RG = pessoaViewModel.rg.Trim();
                pessoa.Email = pessoaViewModel.email;
                _context.Pessoa.Add(pessoa);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPessoa", new { id = pessoa.Id }, pessoa);
            }
            catch (Exception ex)
            {
                //return await Task.FromException<PessoaViewModel>(ex);
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        // DELETE: api/Pessoas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.Id == id);
        }
    }
}

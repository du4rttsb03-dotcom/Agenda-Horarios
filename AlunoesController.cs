using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // <--- Certifique-se de que está aqui
using Microsoft.EntityFrameworkCore;
using AutoEscolaAPI.Data;
using AutoEscolaAPI.Models;

namespace AutoEscolaAPI.Controllers
{
    [Route("api/[controller]")] // <--- Rota da API
    [ApiController] // <--- Atributo de API
    public class AlunoesController : ControllerBase // <--- ControllerBase
    {
        private readonly AutoEscolaContext _context;

        public AlunoesController(AutoEscolaContext context)
        {
            _context = context;
        }

        // GET: api/Alunoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        // GET: api/Alunoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        // POST: api/Alunoes
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);
        }
        // --- Adicionar em AlunoesController.cs ---

        // PUT: api/Alunoes/5 - ATUALIZAR
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id) return BadRequest();

            _context.Entry(aluno).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        // DELETE: api/Alunoes/5 - EXCLUIR
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}

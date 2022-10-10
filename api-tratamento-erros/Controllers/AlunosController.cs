using api_tratamento_erros.ConnectDB;
using api_tratamento_erros.Models;
using api_tratamento_erros.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_tratamento_erros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunosController : Controller
    {
        private readonly DBContext _context;
        public AlunosController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        // [Route("/List/Alunos")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAll()
        {
            var alunos = await _context.Aluno.ToListAsync();
            // throw new Exception("Houve um problema ao obter os alunos.");
            return alunos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAlunoId(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return aluno;
        }

        [HttpPost]
        [ActionName(nameof(CreateAlunoAsync))]
        public async Task<ActionResult<Aluno>> CreateAlunoAsync(Aluno aluno)
        {
            if (_context.Aluno == null)
            {
                return Problem("Entity set DbContext.Aluno is null.");
            }
            _context.Aluno.Add(aluno);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtAction("GetAluno", new { id = aluno.Id });
            // return CreatedAtAction(nameof(aluno), new { id = aluno.Id }, aluno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

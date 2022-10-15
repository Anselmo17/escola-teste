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
    [Route("[controller]")]
    [ApiController]
    public class AlunosController : Controller
    {
        private readonly DBContext _context;
        public AlunosController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAll([FromQuery] int Page = 0, int Size = 5)
        {
            var alunos = await _context.Aluno.Skip(Page)
                                              .Take(Size)
                                              .ToListAsync();
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
        public async Task<ActionResult<Aluno>> CreateAlunoAsync(Aluno aluno)
        {
            var date = DateTime.Now;
            aluno.Created = date;
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

            // colocar o nome do metodo no retorno dentro da string
            return CreatedAtAction("CreateAlunoAsync", new { id = aluno.Id }, aluno);
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

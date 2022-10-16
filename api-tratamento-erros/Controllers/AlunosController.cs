using api_tratamento_erros.ConnectDB;
using api_tratamento_erros.Interface;
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
        private IAlunoRepository alunoRepository;
        public AlunosController(DBContext context, IAlunoRepository alunoRepository)
        {
            _context = context;
            this.alunoRepository = alunoRepository;
        }

        [HttpGet]
        public async Task <List<Aluno>> GetAllAsync([FromQuery] int Page = 0, int Size = 5)
        {
            var alunos = await alunoRepository.GetAllAsync(Page, Size);
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
            var alunoCreated = await alunoRepository.CreateAlunoAsync(aluno);
         
            // colocar o nome do metodo no retorno dentro da string
            return CreatedAtAction("CreateAlunoAsync", new { id = alunoCreated.Id }, alunoCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Aluno>> PutAlunoAsync(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }
           
            try
            {
                var alunoCreated = await alunoRepository.PutAlunoAsync(id, aluno);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("PutAlunoAsync", new { id = id }, aluno);
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await alunoRepository.DeleteAluno(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}

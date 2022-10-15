using api_tratamento_erros.ConnectDB;
using api_tratamento_erros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_tratamento_erros.Interface;

namespace api_tratamento_erros.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DBContext _context;

        public AlunoRepository()
        {
        }

        public AlunoRepository(DBContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Aluno>> GetAllAsync(int Page, int Size)
        {
            var alunos = await _context.Aluno.Skip(Page)
                                               .Take(Size)
                                               .ToListAsync();
            return alunos;
        }

        async Task<List<Aluno>> IAlunoRepository.GetAllAsync(int Page, int Size)
        {
            var alunos = await _context.Aluno.Skip(Page)
                                               .Take(Size)
                                               .ToListAsync();
            return alunos;
        }
    }
}

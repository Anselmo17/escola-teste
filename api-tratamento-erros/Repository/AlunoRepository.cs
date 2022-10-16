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

        async Task<DataAluno> IAlunoRepository.GetAllAsync(int Page, int Size)
        {
            var alunos = await _context.Aluno.Skip(Page)
                                               .Take(Size)
                                               .ToListAsync();
            var total = _context.Aluno.Count();
            // create object
            DataAluno data = new DataAluno();
            data.alunos = alunos;
            data.totalItens = total;
            return data;
        }

        async Task<Aluno> IAlunoRepository.GetAlunoIdAsync(int Id)
        {
            var aluno = await _context.Aluno.FindAsync(Id);
            return aluno;
        }

        async Task<Aluno> IAlunoRepository.CreateAlunoAsync(Aluno aluno)
        {
            _context.Aluno.Add(aluno);

            try
            {
                var result = await _context.SaveChangesAsync();
                return aluno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<Aluno> IAlunoRepository.PutAlunoAsync(int id, Aluno aluno)
        {
            try
            {
                _context.Entry(aluno).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return aluno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<Aluno> IAlunoRepository.DeleteAluno(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return null;
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }
    }
}

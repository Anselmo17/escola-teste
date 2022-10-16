using api_tratamento_erros.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_tratamento_erros.Interface
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> GetAllAsync(int Page, int Size);

        Task<Aluno> GetAlunoIdAsync(int id);

        Task<Aluno> CreateAlunoAsync(Aluno aluno);

        Task<Aluno> DeleteAluno(int id);

        Task<Aluno> PutAlunoAsync(int id, Aluno aluno);
    }
}

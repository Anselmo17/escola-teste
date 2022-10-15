using api_tratamento_erros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_tratamento_erros.Interface
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> GetAllAsync(int Page, int Size);
    }
}

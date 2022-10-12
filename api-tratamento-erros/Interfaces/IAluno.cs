using api_tratamento_erros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_tratamento_erros
{
    public interface IAluno
    {
       // metodos 
        Task<Aluno> GetAll();

        Task<Aluno> GetAlunoId(string id);
    }
}

using api_tratamento_erros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_tratamento_erros.Repository
{
    public class AlunoRepository
    {
        public static List<Aluno> GetAlunos()
        {
            return new List<Aluno>()
            {
                new Aluno { Nome="Marilne", Email="marilene@email.com"},
                new Aluno { Nome="Cassia", Email="Cassia@email.com"},
                new Aluno { Nome="Jose", Email="jose@email.com"},
                new Aluno { Nome="Janete", Email="janjan@email.com"},
                new Aluno { Nome="Miriam", Email="mimi@email.com"},
                new Aluno { Nome="Sandra", Email="sansan@email.com"},
            };
        }
    }
}

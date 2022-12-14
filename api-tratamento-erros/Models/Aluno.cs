using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_tratamento_erros.Models
{
    [Table("tb_alunos")]
    public class Aluno
    {
        public Aluno()
        {
            this.Created = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set;  }

        [Required(ErrorMessage ="O campo Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo precisa ser um email valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime Created { get; set; }

    }

    public class DataAluno
    {
        public List<Aluno> alunos { get; set; }
        public int totalItens { get; set; }
    }

}

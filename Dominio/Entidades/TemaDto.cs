using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class TemaDto
    {
        public int Id { get; set; }
        public string Livro { get; set; }
        public string? Responsavel { get; set; }      // ← NOME (string)
        public DateTime? DataApresentacao { get; set; }
    }
}

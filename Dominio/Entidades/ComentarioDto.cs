using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ComentarioDto
    {
        public int Id { get; set; }
        public int TemaId { get; set; }
        public int UsuarioId { get; set; }
        public string Texto { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class ComentarioModel
    {
        public int Id { get; set; }
        public int TemaId { get; set; }
        public int UsuarioId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }

        // Navigation properties
        public virtual TemaModel Tema { get; set; } = null!;
        public virtual UsuarioModel Usuario { get; set; } = null!;
    }
}

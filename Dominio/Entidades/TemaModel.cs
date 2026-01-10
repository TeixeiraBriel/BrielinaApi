using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class TemaModel
    {
        public int Id { get; set; }
        public string Livro { get; set; } = string.Empty;
        public int? ResponsavelId { get; set; }
        public DateTime? DataApresentacao { get; set; }

        // controle
        public int CriadoPorId { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

        // Navigation property para usuário responsável
        public virtual UsuarioModel? Responsavel { get; set; }
        public virtual UsuarioModel CriadoPor { get; set; } = null!;

        public virtual ICollection<ComentarioModel> Comentarios { get; set; } = new List<ComentarioModel>();
    }
}

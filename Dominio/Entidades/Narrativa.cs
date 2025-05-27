using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Narrativa
    {
        public int IdNarrativas { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Texto { get; set; }
        public string Ramificacoes { get; set; }
        public int Tipo { get; set; }
    }
}
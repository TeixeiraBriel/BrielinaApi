using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Anime
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Episodios { get; set; }

        public string Generos { get; set; }

        public string Completo { get; set; }

        public string Link { get; set; }

        public string LinkImage { get; set; }

        public string DiaLancamento { get; set; }
    }
}

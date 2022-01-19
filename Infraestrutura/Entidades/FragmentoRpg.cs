using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class FragmentoRpg
    {
        public int IdFragmentoHistoria { get; set; }
        public string TituloFragmento { get; set; }
        public string TextoFragmento { get; set; }
        public int IdFragmentoParent { get; set; } = 0;

    }
}

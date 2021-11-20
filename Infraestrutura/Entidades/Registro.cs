using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Registro
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
        public string Grupo { get; set; }
        public int Credito { get; set; }
        public int Tipo { get; set; }
        public string Descricao { get; set; }
        public int Fixa { get; set; }
        public string DataVencimento { get; set; }
    }
}

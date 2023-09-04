using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvgPrograma.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string? Desc { get; set; }

        public float Preco { get; set; }

        public string? Pagamento { get; set; }

        public string? Cliente { get; set; }

        public int? ContatoCliente { get; set; }
    }
}

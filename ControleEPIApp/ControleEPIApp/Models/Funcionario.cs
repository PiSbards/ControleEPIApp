using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ControleEPIApp.Models
{
    public class Funcionario
    {
        public int id { get; set; }
        public int matricula { get; set; }
        public string nome { get; set; }
        public string epi { get; set; }
        public DateTime data_entrega  { get; set; }
        public DateTime data_vencimento { get; set; }
    }
}

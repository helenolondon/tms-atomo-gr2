using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class EnumOV
    {
        public EnumOV(int codigo, string descricao)
        {
            this.Codigo = codigo;
            this.Descricao = descricao;
        }

        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}

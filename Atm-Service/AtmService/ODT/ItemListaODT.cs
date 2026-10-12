using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class ItemListaODT
    {
        public ItemListaODT(EnumOV objetoDeValor)
        {
            this.Codigo = objetoDeValor.Codigo;
            this.Descricao = objetoDeValor.Descricao;
        }

        public ItemListaODT(int codigo)
        {
            this.Codigo = codigo;
        }

        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}

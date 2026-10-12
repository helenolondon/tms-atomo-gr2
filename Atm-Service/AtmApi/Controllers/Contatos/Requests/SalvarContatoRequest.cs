using AtmService.ODT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmApi.Controllers.Contatos.Requests
{
    public class SalvarContatoRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public string Observacoes { get; set; }
        public string Telefone { get; set; }

        public ContatoODT ODT()
        {
            var odt = new ContatoODT();

            odt.Id = this.Id;
            odt.Nome = this.Nome;
            odt.Observacoes = this.Observacoes;
            odt.Telefone = this.Telefone;
            odt.Url = this.Url;

            return odt;
        }
    }
}

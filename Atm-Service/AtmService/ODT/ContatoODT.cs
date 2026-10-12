using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    /// <summary>
    /// Representação de contatos no sistema
    /// </summary>
    public class ContatoODT
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public string Telefone { get; set; }
        public string Observacoes { get; set; }

        internal static ContatoODT NewFromODN(ContatoODN origem)
        {
            return new ContatoODT()
            {
                Id = origem.CodContato,
                Nome = origem.Nome,
                Url = origem.Url,
                Telefone = origem.Telefone,
                Observacoes = origem.Observacoes
            };
        }

        internal ContatoODN ToContatoODN()
        {
            var odn = new ContatoODN();

            odn.CodContato = this.Id;
            odn.Nome = this.Nome;
            odn.Telefone = this.Telefone;
            odn.Url = this.Url;
            odn.Observacoes = this.Observacoes;

            return odn;
        }
    }
}

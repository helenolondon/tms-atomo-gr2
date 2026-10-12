using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class EnderecoODT
    {
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public string Tipo { get; set; }
        public BairroODT Bairro { get; set; }
        public CidadeODT Cidade { get; set; }
        public EstadoODT Estado { get; set; }

        internal void FromODN(LogradouroODN logradouroODN, BairroODN bairro, CidadeODN cidade, EstadoODN estado)
        {
            this.Logradouro = logradouroODN.NomeLogradouro;
            this.Cep = logradouroODN.Cep;
            this.Tipo = logradouroODN.Tipo;
            
            if (bairro != null)
            {
                this.Bairro = new BairroODT()
                {
                    CodigoBairro = bairro.CodigoBairro,
                    NomeBairro = bairro.NomeBairro
                };
            }

            if (cidade != null)
            {
                this.Cidade = new CidadeODT()
                {
                    CodigoCidade = cidade.CodigoCidade,
                    NomeCidade = cidade.NomeCidade
                };
            }

            if(estado!= null)
            {
                this.Estado = new EstadoODT()
                {
                    CodigoEstado = estado.CodigoEstado,
                    NomeEstado = estado.NomeEstado,
                    UF = estado.UF
                };       
            }
        }
    }
}

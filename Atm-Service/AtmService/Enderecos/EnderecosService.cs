using AtmService.Erros;
using AtmService.ODT;
using AtmService.Utils;
using AtmServiceDominio;
using AtmServiceDominio.ObjetosDeDominio.Bairros;
using AtmServiceDominio.ObjetosDeDominio.Cidades;
using AtmServiceDominio.ObjetosDeDominio.Estados;
using AtmServiceDominio.ObjetosDeDominio.Logradouros;
using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmService.Enderecos
{
    public class EnderecosService : AtmService, IEnderecosService
    {
        private readonly ILogradourosOD logradourosOD;
        private readonly ICidadesOD cidadesOD;
        private readonly IBairrosOD bairrosOD;
        private readonly IEstadosOD estadosOD;

        public EnderecosService(ILogradourosOD logradourosOD, ICidadesOD cidadesOD, IBairrosOD bairrosOD,
            IEstadosOD estadosOD)
        {
            this.logradourosOD = logradourosOD;
            this.cidadesOD = cidadesOD;
            this.bairrosOD = bairrosOD;
            this.estadosOD = estadosOD;
        }

        public CidadeODT ObterCidadePorCodigo(int codCidade)
        {
            var odn = this.cidadesOD.ObterCidadePorId(codCidade);

            if(odn != null)
            {
                return CidadeODT.NewFromODN(odn);
            }

            return null;
        }

        public EnderecoODT ObterEnderecoPorCep(string cep)
        {
            var odn = this.logradourosOD.ObterPorCep(cep);
            BairroODN bairroODN = null;
            CidadeODN cidadeODN  = null;
            EstadoODN estadoODN = null;

            if (odn == null)
            {
                return null;
            }

            if(odn.CodBairro > 0)
            {
                bairroODN = this.bairrosOD.ObterBairroPorId(odn.CodBairro);
            }

            if(bairroODN != null && bairroODN.CodigoCidade > 0)
            {
                cidadeODN = this.cidadesOD.ObterCidadePorId(bairroODN.CodigoCidade);            
            }

            if(cidadeODN != null && cidadeODN.CodigoEstado > 0)
            {
                estadoODN = estadosOD.ObterEstadoPorId(cidadeODN.CodigoEstado);
            };

            var odt = new EnderecoODT();
            odt.FromODN(odn, bairroODN, cidadeODN, estadoODN);

            return odt;
        }

        public List<EstadoODT> ObterEstados()
        {
            var odns = this.estadosOD.ObterEstados();
            var odts = new List<EstadoODT>();

            if(odns != null && odns.Count > 0)
            {
                odts = odts.FromListaODN(odns);
            }

            return odts;
        }

        public List<CidadeODT> ObterCidadesPorEstado(int codEstado)
        {
            var estado = this.estadosOD.ObterEstados()
                ?.Where(estado => estado.CodigoEstado == codEstado)
                ?.SingleOrDefault();

            if(estado == null)
            {
                throw new AtmServiceNaoEncontradoSE("Estado não existe");
            }

            var odns = this.cidadesOD.ObterCidadesPorEstado(codEstado);
            var odts = new List<CidadeODT>();

            if (odns != null && odns.Count > 0)
            {
                odts = odts.FromListaODN(odns);
            }

            return odts;
        }

        public List<CidadeODT> ObterCidadesPorNome(string nomeCidade, string uf = null)
        {
            EstadoODN estadoODN = null;

            if(uf != null)
            {
                estadoODN = this.estadosOD.ObterEstados()
                    .Where(estado => estado.UF.ToUpper() == uf.ToUpper())
                    .SingleOrDefault();
            };

            var odns = this.cidadesOD.ObterCidades(nomeCidade, estadoODN?.CodigoEstado);
            var odts = new List<CidadeODT>();

            if (odns != null && odns.Count > 0)
            {
                odts = odts.FromListaODN(odns);
            }

            return odts;
        }

        public List<BairroODT> ObterBairrosPorCidade(int codCidade)
        {
            var bairros = this.bairrosOD.ObterBairroPorCidade(codCidade);

            if (bairros == null || bairros.Count == 0)
            {
                return null;
            }

            var odns = this.bairrosOD.ObterBairroPorCidade(codCidade);
            var odts = new List<BairroODT>();

            if (odns != null && odns.Count > 0)
            {
                odts = odts.FromListaODN(odns);
            }

            return odts;
        }
    }
}

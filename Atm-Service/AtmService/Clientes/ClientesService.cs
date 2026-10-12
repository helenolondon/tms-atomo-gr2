using AtmService.ODT;
using AtmService.Utils;
using AtmServiceDominio.ObjetosDeDominio.Clientes;
using AtmServiceDominio.ODN;
using AtmServiceDominio.ODN.Tipos.Fiscal;
using AtmServiceDominio.ODN.Tipos.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.Clientes
{
    public class ClientesService: AtmService, IClientesService
    {
        private IClientesOD clientesOD = null;

        public ClientesService(IClientesOD clientesOD)
        {
            this.clientesOD = clientesOD;
        }

        public ClienteODT ObterCliente(int codCliente)
        {
            var odn = this.clientesOD.ObterClientePorCodigo(codCliente);

            if(odn != null)
            {
                var odt = new ClienteODT();
                odt.FromODN(odn);

                return odt;
            };

            return null;
        }

        public ClienteODT SalvarCliente(ClienteODT cliente)
        {
            ClienteODN odn = null;

            this.ExecutarAcaoDeDominio(() =>
            {
                odn = this.clientesOD.Salvar(cliente.ToODN()) as ClienteODN;

                
            });
            
            if (odn == null)
            {
                return null;
            };

            var odt = new ClienteODT();
            odt.FromODN(odn);

            return odt;
        }

        public void Excluir(int codCliente)
        {
            this.clientesOD.Excluir(codCliente);
        }

        public List<ClienteODT> ListarClientes()
        {
            var odns = this.clientesOD.ObterClientes();

            if(odns == null || odns.Count == 0)
            {
                return null;
            };

            var odts = new List<ClienteODT>();

            odts.FromListaODN(odns);

            return odts;
        }

        public List<ClienteODT> ListarClientesPorNomeOuCnfCnpj(string nomeCpfCnpj)
        {
            var odns = this.clientesOD.ObterClientesPorNomeOuCpfCnpj(nomeCpfCnpj.Numeros());

            var odts = new List<ClienteODT>();

            if (odns != null && odns.Count > 0)
            {
                odts.FromListaODN(odns);
            };

            return odts;
        }

        public List<ClienteODT> ListarClientes(int codCliente = 0, string nome = null, int? tiposDePessoa = null, int cnae = 0, string inscricaoEstadual = null,
            bool? microEmpresa = null, string telefone = null, string contato = null, string identidade = null, string cpf = null, int[] situacaoInscricaoEstadual = null,
            DateTime? dataNascimento = null, int[] estadosCivis = null, int[] codUfs = null, int[] codCidades = null, string razaoSocial = null)
        {
            var sitInscricaoEstadual = situacaoInscricaoEstadual != null ? situacaoInscricaoEstadual.Select(s => (SituacaoInscricaoEstadual)s).ToArray() : null;
            var estEstadosCivis = estadosCivis != null ? estadosCivis.Select(s => (EstadoCivil)s).ToArray() : null;
            var tpTipoPessoa = tiposDePessoa != null ? (TipoDePessoa)tiposDePessoa : new Nullable<TipoDePessoa>();

            var odns = this.clientesOD.ObterClientes(codCliente, nome, tpTipoPessoa, cnae, inscricaoEstadual, microEmpresa, telefone, contato, identidade, cpf, sitInscricaoEstadual,
            dataNascimento, estEstadosCivis, codUfs = null, codCidades, razaoSocial);

            var odts = new List<ClienteODT>();

            if (odns != null && odns.Count > 0)
            {
                odts.FromListaODN(odns);
            };

            return odts;
        }

        public List<ItemListaODT> ListarTiposDePessoa()
        {
            var ovs = this.clientesOD.ListarTiposDePessoa();
            var odts = new List<ItemListaODT>();

            if (ovs != null)
            {
                foreach (var ov in ovs)
                {
                    odts.Add(new ItemListaODT(ov));
                }
            }

            return odts;
        }
        
        public List<ItemListaODT> ListarEstadosCivis()
        {
            var ovs = this.clientesOD.ObterEstadosCivilDeCliente();
            var odts = new List<ItemListaODT>();

            if (ovs != null)
            {
                foreach (var ov in ovs)
                {
                    odts.Add(new ItemListaODT(ov));
                }
            }

            return odts;
        }
    }
}

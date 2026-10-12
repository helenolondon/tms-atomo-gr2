using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN;
using AtmServiceDominio.ODN.Tipos.Pessoas;
using AtmServiceDominio.ODN.Tipos.Fiscal;
using AtmServiceDominio.Utils;
using AtmServiceInfra;
using AtmServiceInfra.Clientes;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Clientes
{
    public class ClientesOD : AtmObjetoDominio<ClienteODP>, IClientesOD
    {
        public ClientesOD(IRepositorio<ClienteODP> repositorio) : base(repositorio)
        {

        }

        public ClienteODN ObterClientePorCodigo(int codigo)
        {
            return this.Retorna1<ClienteODN>(this.ObterClientes(codCliente: codigo));
        }

        public List<ClienteODN> ObterClientes()
        {
            return this.ObterClientes(codCliente: 0);
        }

        public List<ClienteODN> ObterClientesPorNomeOuCpfCnpj(string nomeOuCpfCnpj)
        {
            return this.ObterClientes(tiposDePessoa: TipoDePessoa.PessoaJuridica,  nomeOuCpfCnpj: nomeOuCpfCnpj);
        }
        
        public List<ClienteODN> ObterClientes(int codCliente = 0, string nome = null, TipoDePessoa? tiposDePessoa = null, int cnae = 0, string inscricaoEstadual = null,
            bool? microEmpresa = null, string telefone = null, string contato = null, string identidade = null, string cpf = null, SituacaoInscricaoEstadual[] situacaoInscricaoEstadual = null,
            DateTime? dataNascimento = null, EstadoCivil[] estadosCivis = null, int[] codUfs = null, int[] codCidades = null, string razaoSocial = null, string nomeOuCpfCnpj = null)
        {
            var intTipoPessoa = tiposDePessoa != null ? (int)tiposDePessoa : new Nullable<int>();
            var intMicroEmpresa = microEmpresa != null ? microEmpresa.Value? 1: 0 : new Nullable<int>();

            int[] intSituacaoIE = null;

            if (situacaoInscricaoEstadual != null)
            {
                intSituacaoIE = situacaoInscricaoEstadual.Select(st => (int)st).ToArray();
            }
            
            int[] intEstadosCivil = null;

            if (estadosCivis != null)
            {
                intEstadosCivil = estadosCivis.Select(ec => (int)ec).ToArray();
            }

            var odps = (this.repositorio as ClientesRepositorio).ObterClientes(codCliente, nome, intTipoPessoa, cnae, inscricaoEstadual,
            microEmpresa, telefone, contato, identidade, cpf, intSituacaoIE, dataNascimento, intEstadosCivil, codUfs, codCidades, razaoSocial, nomeOuCpfCnpj);
            
            if(odps == null || odps.Count == 0)
            {
                return null;
            }

            var odns = new List<ClienteODN>();

            odns = odns.FromListaODP(odps);

            return odns;
        }

        public List<EnumOV> ListarTiposDePessoa()
        {
            return new List<EnumOV>()
            {
                new EnumOV((int)TipoDePessoa.PessoaFisica, "Pessoa física"),
                new EnumOV((int)TipoDePessoa.PessoaJuridica, "Pessoa jurídica")
            };
        }
        
        public List<EnumOV> ObterEstadosCivilDeCliente()
        {
            return new List<EnumOV>()
            {
                new EnumOV((int)EstadoCivil.Casado, "Casado(a)"),
                new EnumOV((int)EstadoCivil.Solteiro, "Solteiro(a)"),
                new EnumOV((int)EstadoCivil.Divorciado, "Divorciado(a)"),
                new EnumOV((int)EstadoCivil.Viuvo, "Viúvo(a)"),
                new EnumOV((int)EstadoCivil.Outros, "Outros")
            };
        }

        protected internal override void validar(ObjetoDeNegocio obj, bool inserindo)
        {
            var odn = (obj as ClienteODN);

            if (!inserindo)
            {
                var old = this.repositorio.Obter(odn.CodCliente);

                if ((old != null) && (old.TipoPessoa != (int)odn.TipoPessoa))
                {
                    throw new ErroDeValidacaoDE("Não é permitido alterar o tipo de pessoa");
                }
            }            
        }

        protected internal override void AntesDeSalvar(ClienteODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (obj.TipoPessoa == (int)TipoDePessoa.PessoaFisica)
            {
                obj.CnaePrincipal = null;
                obj.InscricaoSuframa = null;
                obj.InscricaoMunicipal = null;
                obj.MicroEmpresa = false;
                obj.RazaoSocial = null;
            }
            else if (obj.TipoPessoa == (int)TipoDePessoa.PessoaJuridica)
            {
                obj.Identidade = null;
                obj.OrgaoExpedidor = null;
                obj.Profissao = null;
                obj.DataNascimento = null;
                obj.EstadoCivil = null;
                obj.DataEmissaoIdentidade = null;
                obj.Nacionalidade = null;
            };

            if (inserindo)
            {
                obj.CodCliente = this.repositorio.ObterProximoValorDeSequencia("TD_CLIENTES_SEQ");
            }
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var temp = obj as ClienteODN;

            if (temp.CodCliente <= 0)
            {
                return null;
            }

            return new object[] { temp.CodCliente };
        }
    }
}

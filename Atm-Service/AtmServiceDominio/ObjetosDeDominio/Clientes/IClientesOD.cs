using AtmServiceDominio.ODN;
using AtmServiceDominio.ODN.Tipos.Pessoas;
using AtmServiceDominio.ODN.Tipos.Fiscal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Clientes
{
    public interface IClientesOD: IAtmObjetoDominio
    {
        ClienteODN ObterClientePorCodigo(int codigo);

        List<ClienteODN> ObterClientes();
        public List<ClienteODN> ObterClientesPorNomeOuCpfCnpj(string nomeOuCpfCnpj);
        List<ClienteODN> ObterClientes(int codCliente = 0, string nome = null, TipoDePessoa? tipoDePessoa = null, int cnae = 0, string inscricaoEstadual = null,
            bool? microEmpresa = null, string telefone = null, string contato = null, string identidade = null, string cpf = null, SituacaoInscricaoEstadual[] situacaoInscricaoEstadual = null,
            DateTime? dataNascimento = null, EstadoCivil[] estadosCivis = null, int[] codUfs = null, int[] codCidades = null, string razaoSocial = null, string nomeOuCpfCnpj = null);

        List<EnumOV> ListarTiposDePessoa();
        List<EnumOV> ObterEstadosCivilDeCliente();
    }
}
